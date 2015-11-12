using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HardPressurePlate : MonoBehaviour {

    public GameObject obj;
    bool activated;

    int numActive = 0;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<ObjectInfo>() != null && coll.gameObject.GetComponent<ObjectInfo>().weight > 5)
        {
            activated = true;
            obj.GetComponent<Activate>().Invoke();
            numActive++;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<ObjectInfo>() != null && coll.gameObject.GetComponent<ObjectInfo>().weight > 5)
        {
            numActive--;
        }
        if(numActive == 0)
        {
            activated = false;
            obj.GetComponent<Activate>().Uninvoke();
        }

    }
}
