using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour
{
    public string[] text;
    int index = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0)) // left click
        {
            if (index < text.Length - 1)
            {
                index++;
            }
            else
            {
                Application.LoadLevel("tiles");
            }
        }
	}

    void OnGUI()
    {
        GUI.skin.box.alignment = TextAnchor.MiddleCenter;
        GUI.Box(new Rect(Screen.width * 0.4f,Screen.height * 0.2f,Screen.width*0.5f,Screen.height * 0.6f), text[index]);
    }
}
