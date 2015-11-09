using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
public class WallCrab : MonoBehaviour
{
    InputHandler inputHandler;

    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .1f;
    float accelerationTimeGrounded = .05f;

    bool isOnWallRight;
    bool isOnWallLeft;

    float moveSpeed = 6;

    float jumpVelocity;
    float gravity;
    Vector3 velocity;
    float velocityXSmoothing;

    Controller2D controller;

    void Start()
    {
        controller = GetComponent<Controller2D>();
        inputHandler = GetComponent<InputHandler>();
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
    }

    void Update()
    {
        Vector2 input = Vector2.zero;

        // This code is for just running into the wall and you climb up it.

        /*
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        if (inputHandler.shouldUpdate)
        {
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);

        Debug.Log("Is the cube colliding right: " + controller.collisions.right);
        if (controller.collisions.right)
        {
            velocity.y = velocity.x;
            //velocity.x = ;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }
        controller.Move(velocity * Time.deltaTime);
        */
        
        if (inputHandler.shouldUpdate)
        {
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.right)
            {
                isOnWallRight = !isOnWallRight;
                isOnWallLeft = false;
            }
            if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.left)
            {
                isOnWallLeft = !isOnWallLeft;
                isOnWallRight = false;
            }
            
        }

        Debug.Log(isOnWallRight + ", " + controller.collisions.right);

        if (isOnWallRight)
        {
            float inputVelocity = 0;
            if (input.x > 0 && input.y > 0)
                inputVelocity = Mathf.Max(input.x, input.y);
            else if (input.x < 0 && input.y < 0)
                inputVelocity = Mathf.Min(input.x, input.y);

            float targetVelocityX = inputVelocity * moveSpeed;
            velocity.y = Mathf.SmoothDamp(velocity.y, targetVelocityX, ref velocityXSmoothing, accelerationTimeGrounded);
            velocity.x = velocity.y;
            controller.Move(velocity * Time.deltaTime);
        }
        else if (!isOnWallLeft && !isOnWallRight)
        {
            if (controller.collisions.above || controller.collisions.below)
            {
                velocity.y = 0;
            }
            float targetVelocityX = input.x * moveSpeed;
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
         
    }
}
