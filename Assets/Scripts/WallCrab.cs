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

        if (inputHandler.shouldUpdate)
        {
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.right)
            {
                isOnWallRight = !isOnWallRight;
                isOnWallLeft = false;
                velocity = Vector2.zero;
            }
            if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.left)
            {
                isOnWallLeft = !isOnWallLeft;
                isOnWallRight = false;
                velocity = Vector2.zero;
            }
        }

        if (isOnWallRight)
        {
            if (!controller.collisions.right)
                isOnWallRight = false;
            float inputVelocity = Mathf.Clamp((input.x + input.y), -1, 1);
            float targetVelocityX = inputVelocity * moveSpeed;
            velocity.y = Mathf.SmoothDamp(velocity.y, targetVelocityX, ref velocityXSmoothing, accelerationTimeGrounded);
            velocity.x = Mathf.Abs(velocity.y + 0.05f);
            controller.Move(velocity * Time.deltaTime);
            Debug.Log(velocity);
        }
        else if (isOnWallLeft)
        {
            if (!controller.collisions.left)
                isOnWallLeft = false;
            float inputVelocity = Mathf.Clamp((input.x + input.y), -1, 1);
            float targetVelocityX = -inputVelocity * moveSpeed;
            velocity.y = Mathf.SmoothDamp(velocity.y, targetVelocityX, ref velocityXSmoothing, accelerationTimeGrounded);
            velocity.x = -Mathf.Abs(velocity.y - 0.05f);
            controller.Move(velocity * Time.deltaTime);
            Debug.Log(velocity);
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
