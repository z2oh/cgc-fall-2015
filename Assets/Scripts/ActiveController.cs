using UnityEngine;
using System.Collections;

public class ActiveController : MonoBehaviour
{
    // This is the list of all possible player controlled characters.
    GameObject[] playerControlledCharacters;

    // This is the index of the actively controlled character.
    int index;

	void Start ()
    {
        // All the potentially player controlled characters are added to this array.
        playerControlledCharacters = GameObject.FindGameObjectsWithTag("PlayerControlled");

        // The main Player is found and the index is set to that player.
        for (int i = 0; i < playerControlledCharacters.Length; i++)
        {
            if(playerControlledCharacters[i].name == "Player")
            {
                index = i;
            }
        }

        // Set the current active player to update.
        playerControlledCharacters[index].GetComponent<InputHandler>().shouldUpdate = true;
	}
	
	void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.Tab))
        {
            playerControlledCharacters[index].GetComponent<InputHandler>().shouldUpdate = false;
            index++;
            if (index >= playerControlledCharacters.Length)
                index = 0;
            playerControlledCharacters[index].GetComponent<InputHandler>().shouldUpdate = true;
            GetComponent<FollowPlayer>().player = playerControlledCharacters[index];
        }
	}
}
