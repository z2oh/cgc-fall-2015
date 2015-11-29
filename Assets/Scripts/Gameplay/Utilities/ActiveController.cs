using UnityEngine;
using System.Collections;

public class ActiveController : MonoBehaviour
{
    // This is the list of all possible player controlled characters.
    public GameObject[] playerControlledCharacters;

    public bool shouldLoad = true;

    // This is the index of the actively controlled character.
    int index;

    float rTime = 0f;

	void Update()
    {
        if(shouldLoad && gameObject.GetComponent<LoadLevel>().loaded)
        {
            // All the potentially player controlled characters are added to this array.
            playerControlledCharacters = GameObject.FindGameObjectsWithTag("PlayerControlled");

            // The main Player is found and the index is set to that player.
            for (int i = 0; i < playerControlledCharacters.Length; i++)
            {
                // When we find the main Player, we save its index.
                if (playerControlledCharacters[i].name == "Player")
                {
                    index = i;
                }
            }

            // Set the current active player to update.
            playerControlledCharacters[index].GetComponent<InputHandler>().shouldUpdate = true;
            shouldLoad = false;
        }
	    else if(Input.GetKeyDown(KeyCode.Tab))
        {
            // We disable the currently active player.
            playerControlledCharacters[index].GetComponent<InputHandler>().shouldUpdate = false;
            // We increment the index.
            index++;
            // If we reach the end of the array, we loop back to the start of the array.
            if (index >= playerControlledCharacters.Length)
                index = 0;
            // We enable the next active player.
            playerControlledCharacters[index].GetComponent<InputHandler>().shouldUpdate = true;
            // We set the camera's 'FollowPlayer' script to follow the currently active player.
            GetComponent<FollowPlayer>().player = playerControlledCharacters[index];
        }
        if(Input.GetKey(KeyCode.R))
        {
            rTime += Time.deltaTime;
            if (rTime > 1)
            {
                gameObject.GetComponent<LoadLevel>().Load();
                shouldLoad = true;
                rTime = 0;
            }

        }
        else
        {
            rTime = 0;
        }
        // TODO: add 1-9 control groups to select crabs.
	}
}
