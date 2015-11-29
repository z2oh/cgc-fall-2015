using UnityEngine;
using System.Collections;

public class Activator : MonoBehaviour {

    public bool door;
    public Material doorOpen;
    public Material doorClosed;

    int numActivations;
    bool activated;

    public void Activate()
    {
        numActivations++;
        if (!activated)
        {
            if (door)
            {
                GetComponent<MeshRenderer>().material = doorOpen;
                GetComponent<BoxCollider2D>().enabled = false;
                activated = true;
            }
        }
    }

    public void Deactivate()
    {
        numActivations--;
        if(numActivations == 0 && activated)
        {
            if (door)
            {
                GetComponent<MeshRenderer>().material = doorClosed;
                GetComponent<BoxCollider2D>().enabled = true;
                activated = false;
            }
        }
    }
}
