using UnityEngine;
using System.Collections;

public class TutorialButton : MonoBehaviour
{

    public void Pressed()
    {
        GameObject.DontDestroyOnLoad(GameObject.Find("PathHolder"));
        Application.LoadLevel("main");
    }

}
