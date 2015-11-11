using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
[RequireComponent (typeof (InputHandler))]
public class Player : MonoBehaviour
{
    // The input handler used to determine if this character should accept input.
    InputHandler inputHandler;

    // jumpHeight and timeToJumpApex are used to determine the correct gravity and jump forces.
    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;

    // accelerationTimeAirborne and accelerationTimeGrounded have to do with the 'smoothing' factor when changing directions. It should be easier to change directions when on the ground.
    float accelerationTimeAirborne = .1f;
    float accelerationTimeGrounded = .05f;

    // moveSpeed is the speed of the player.
    float moveSpeed = 6;

    // These values are used to update position and velocity every frame.
    float jumpVelocity;
    float gravity;
    Vector3 velocity;
    float velocityXSmoothing;

    // The controller that is used in collision detection.
    Controller2D controller;

	void Start ()
    {
        // We set the controller and input handler for this object, as well as calculating the gravity and jump velocity.
        controller = GetComponent<Controller2D>();
        inputHandler = GetComponent<InputHandler>();
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
	}

    void Update()
    {
        // If the player is actively colliding with something above or below it, the velocity in the y direction is reset to 0.
        if(controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        // We make the input vector and set it to zero.
        Vector2 input = Vector2.zero;

        // If this is the active player object, we accept input.
        if (inputHandler.shouldUpdate)
        {
            // We grab the input values from the active controller.
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            // If space is pressed and the player is touching the ground, we set the y velocity to be jump velocity.
            if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
            {
                velocity.y = jumpVelocity;
            }
        }

        // We find the target velocity for x.
        float targetVelocityX = input.x * moveSpeed;
        // We move the velocity in the x direction closer to the target x velocity. This makes changing directions much smoother. We use different acceleration values for when the player is airborne or on the ground.
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        // We apply gravity to the player.
        velocity.y += gravity * Time.deltaTime;
        // We send the calculated velocity vector to the controller which will actually move the object.
        controller.Move(velocity * Time.deltaTime);
    }
}
