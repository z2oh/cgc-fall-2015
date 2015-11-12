using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Button : MonoBehaviour
{
    public GameObject obj;
    bool activated;

    Vector3 oldScale;
    List<Collider2D> activeCollisions = new List<Collider2D>();

    void Start()
    {
        activated = false;
    }

	void Update ()
    {
	    if(activeCollisions.Count > 0)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                foreach(Collider2D coll in activeCollisions)
                {
                    if(coll.gameObject.GetComponent<InputHandler>().shouldUpdate)
                    {
                        activated = !activated;
                        if (activated)
                        {
                            oldScale = transform.localScale;
                            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 0.5f, transform.localScale.z);
                            obj.GetComponent<Activator>().Activate();
                        }
                        else
                        {
                            transform.localScale = oldScale;
                            obj.GetComponent<Activator>().Deactivate();
                        }
                    }
                }
            }
        }
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        activeCollisions.Add(coll);
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        activeCollisions.Remove(coll);
    }
}
