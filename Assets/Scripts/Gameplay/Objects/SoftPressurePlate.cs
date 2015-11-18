using UnityEngine;
using System.Collections;

public class SoftPressurePlate : MonoBehaviour
{
    public GameObject obj;

    int numActive = 0;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<ObjectInfo>() != null)// && coll.gameObject.GetComponent<ObjectInfo>().weight > 5)
        {
            numActive++;
            if (numActive == 1)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 0.5f, transform.localScale.z);
                obj.GetComponent<Activator>().Activate();
            }
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<ObjectInfo>() != null)// && coll.gameObject.GetComponent<ObjectInfo>().weight > 5)
        {
            numActive--;
            if (numActive == 0)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 2f, transform.localScale.z);
                obj.GetComponent<Activator>().Deactivate();
            }
        }
    }
}
