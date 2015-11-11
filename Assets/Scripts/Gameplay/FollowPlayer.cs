using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{

    // This is the current player that the camera is following.
    public GameObject player;

    // This is the amount of time it takes to move the camera. This is less noticeable when moving around, but very noticable when switching characters.
    public float timeToMove = 0.2f;

    // This is the maximum distance that the camera can be away from the player before moving. This makes it look a little bit smoother.
    public float maxDistance = 1;

	void Update ()
    {
        // The player and camera positions are put into Vector2 objects.
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 cameraPos = new Vector2(transform.position.x, transform.position.y);
        // The distance from the two objects is calculated.
        float distance = Vector2.Distance(cameraPos, playerPos);
        // The fraction of total that the camera should move (time elapsed/total time it should take) is calculated.
        float timeRatio = Time.deltaTime / timeToMove;
        // If the distance is greater than the maximum allowed distance, we move the camera.
        if (distance > maxDistance)
        {
            // The camera position is updated.
            // First, we calculate a vector that points from the camera to the player. This vector is then multiplied by the time ratio, to figure out how far the camera needs to be translated.
            // Because this vector is a direction vector pointing from the origin, it must be translated by the current camera position.
            cameraPos = (playerPos - cameraPos) * timeRatio + cameraPos;
        }
        // The camera's actual position is updated to the position that we calculated.
        transform.position = new Vector3(cameraPos.x, cameraPos.y, -10);
    }
}
