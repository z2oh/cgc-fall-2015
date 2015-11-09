using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    public GameObject player;

    static float timeToMove = 0.2f;
    static float maxDistance = 1;

	void Update ()
    {
        UpdateCameraPosition();
	}

    void UpdateCameraPosition()
    {
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 cameraPos = new Vector2(transform.position.x, transform.position.y);
        float distance = Vector2.Distance(cameraPos, playerPos);
        float timeRatio = Time.deltaTime / timeToMove;
        if(distance > maxDistance)
            cameraPos = (playerPos - cameraPos).normalized * distance * timeRatio + cameraPos;
        transform.position = new Vector3(cameraPos.x, cameraPos.y, -10);
    }
}
