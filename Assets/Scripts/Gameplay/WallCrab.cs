using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
public class WallCrab : MonoBehaviour
{
    // The input handler used to determine if this character should accept input.
    InputHandler inputHandler;

    // jumpHeight and timeToJumpApex are used to determine the correct gravity and jump forces.
    // Even though the wall crab cannot jump, this is here in order to calculate the correct gravity.
    // This should probably be changed.
    // Probably.
    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;

    // accelerationTimeAirborne and accelerationTimeGrounded have to do with the 'smoothing' factor when changing directions. It should be easier to change directions when on the ground.
    float accelerationTimeAirborne = .1f;
    float accelerationTimeGrounded = .05f;

    // These bools keep track of whether or not the crab is 'attatched' to the wall.
    bool isOnWallRight;
    bool isOnWallLeft;

    // moveSpeed is the speed of the crab.
    float moveSpeed = 6;

    // These values are used to update position and velocity every frame.
    float jumpVelocity;
    float gravity;
    Vector3 velocity;
    float velocityXSmoothing;

    // The controller that is used in collision detection.
    Controller2D controller;


    void Start()
    {
        // We set the controller and input handler for this object, as well as calculating the gravity and jump velocity.
        controller = GetComponent<Controller2D>();
        inputHandler = GetComponent<InputHandler>();
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
    }

    void Update()
    {
        // We make the input vector and set it to zero.
        Vector2 input = Vector2.zero;

        // If this is the active player object, we accept input.
        if (inputHandler.shouldUpdate)
        {
            // We grab the input values from the active controller.
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            // If the player presses space and is colliding with some object to the right, then the isOnWallRight bool is toggled.
            if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.right)
            {
                // isOnWallRight is toggled.
                isOnWallRight = !isOnWallRight;
                isOnWallLeft = false;
                // The velocity is zeroed out. This is so the crab can 'grab' the wall by pressing space as the crab is falling.
                velocity = Vector2.zero;
            }
            // If the player presses space and is colliding with some object to the left, then the isOnWallLeft bool is toggled.
            if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.left)
            {
                // isOnWallLeft is toggled.
                isOnWallLeft = !isOnWallLeft;
                isOnWallRight = false;
                // The velocity is zeroed out. This is so the crab can 'grab' the wall by pressing space as the crab is falling.
                velocity = Vector2.zero;
            }
        }

        // Depending on if the crab is holding on to a right wall, a left wall, or no wall, different update steps are performed.
        if (isOnWallRight)
        {
            // If the crab is not colliding with the wall, we set the isOnWallRight bool to false.
            if (!controller.collisions.right)
                isOnWallRight = false;
            // The input velocity is clamped by the summation of the x and y inputs. This means that 'right' and 'up' do the same thing, and 'left' and 'down' do the same thing.
            float inputVelocity = Mathf.Clamp((input.x + input.y), -1, 1);
            // The target velocity is found. This is actually the target velocity for the y value.
            float targetVelocityX = inputVelocity * moveSpeed;
            // The y velocity is calculated.
            velocity.y = Mathf.SmoothDamp(velocity.y, targetVelocityX, ref velocityXSmoothing, accelerationTimeGrounded);
            // The x velocity is set to the absoulte value of the y velocity with some added. The 0.05f added onto the velocity is to ensure that the crab keeps colliding with the wall even if the player stops input.
            velocity.x = Mathf.Abs(velocity.y + 0.05f);
            // The actual velocity is sent to the controller to move the crab.
            controller.Move(velocity * Time.deltaTime);
        }
        else if (isOnWallLeft)
        {
            // If the crab is not colliding with the wall, we set the isOnWallLeft bool to false.
            if (!controller.collisions.left)
                isOnWallLeft = false;
            // The input velocity is clamped by the summation of the x and y inputs. This means that 'right' and 'up' do the same thing, and 'left' and 'down' do the same thing.
            float inputVelocity = Mathf.Clamp((input.x + input.y), -1, 1);
            // The target velocity is found. This is actually the target velocity for the y value. We negate inputVelocity because we are now moving to the left.
            float targetVelocityX = -inputVelocity * moveSpeed;
            // The y velocity is calculated.
            velocity.y = Mathf.SmoothDamp(velocity.y, targetVelocityX, ref velocityXSmoothing, accelerationTimeGrounded);
            // The x velocity is always negative. This is to ensure that the crab never stops colliding with the wall.
            velocity.x = -Mathf.Abs(velocity.y - 0.05f);
            // The actual velocity is sent to the controller to move the crab.
            controller.Move(velocity * Time.deltaTime);
        }
        else if (!isOnWallLeft && !isOnWallRight)
        {
            // If the player is actively colliding with something above or below it, the velocity in the y direction is reset to 0.
            if (controller.collisions.above || controller.collisions.below)
            {
                velocity.y = 0;
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
}
