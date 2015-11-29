using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class PathHolderScript : MonoBehaviour
{
    public string path = "C:\\Users\\Jeremy\\Desktop\\levels\\level00.lvl";
    public GameObject textInput;

    public void PressedTut()
    {
        GameObject.DontDestroyOnLoad(GameObject.Find("PathHolder"));
        print(File.Exists(path) + ", " + path);
        if (File.Exists(path))
            Application.LoadLevel("main");
    }

    public void PressedLoad()
    {
        GameObject.DontDestroyOnLoad(GameObject.Find("PathHolder"));
        path = "C:\\Users\\Jeremy\\Desktop\\levels\\" + textInput.GetComponent<Text>().text + ".lvl";
        if(File.Exists(path))
            Application.LoadLevel("main");
    }
}
