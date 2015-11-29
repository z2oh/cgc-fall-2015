using UnityEngine;
using System.Collections;

public class Finish : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<ObjectInfo>().weight > 5)
        {
            GameObject.Destroy(GameObject.Find("PathHolder"));
            Application.LoadLevel("main_menu");
        }
    }
}
