using UnityEngine;
using System.Collections;

public class RainbowText : MonoBehaviour
{
    Material textMaterial;
    static float frequency = 0.1f;
    static int i = 0;
    static int j = 0;

	// Use this for initialization
	void Start () {
        textMaterial = GetComponent<SpriteRenderer>().material;
    }
	
	void Update ()
    {
        if (j < 2)
            j++;
        else
        {
            j = 0;
            i++;
            float red = Mathf.Sin(frequency * i + 0);
            float green = Mathf.Sin(frequency * i + 2);
            float blue = Mathf.Sin(frequency * i + 4);
            textMaterial.color = new Color(red, green, blue);
        }
	}
}
