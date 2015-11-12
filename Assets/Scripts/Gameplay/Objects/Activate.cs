using UnityEngine;
using System.Collections;

public class Activate : MonoBehaviour {

    public bool door;

    bool invoked;
    Vector2 oldScale;

    public void Invoke()
    {
        if (!invoked)
        {
            if (door)
            {
                oldScale = transform.localScale;
                transform.localScale *= 0;
                invoked = true;
            }
        }
    }

    public void Uninvoke()
    {
        if(invoked)
        {
            if (door)
            {
                transform.localScale = oldScale;
                invoked = false;
            }
        }
    }
}
