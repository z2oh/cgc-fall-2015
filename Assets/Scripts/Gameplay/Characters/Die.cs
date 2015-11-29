using UnityEngine;
using System.Collections;

public class Die : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.layer == 9 || coll.gameObject.layer == 10)
        {
            GameObject cam = GameObject.Find("Main Camera");
            cam.GetComponent<ActiveController>().shouldLoad = true;
            cam.GetComponent<LoadLevel>().Load();
        }
    }
}
