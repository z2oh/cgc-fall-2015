using UnityEngine;
using System.Collections;

public class Activator : MonoBehaviour {

    public bool door;
    Vector2 oldScale;

    int numActivations;
    bool activated;

    public void Activate()
    {
        numActivations++;
        if (!activated)
        {
            if (door)
            {
                oldScale = transform.localScale;
                transform.localScale *= 0;
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
                transform.localScale = oldScale;
                activated = false;
            }
        }
    }
}
