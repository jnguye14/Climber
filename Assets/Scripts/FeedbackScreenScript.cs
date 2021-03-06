using UnityEngine;
using System.Collections;

public class FeedbackScreenScript : MonoBehaviour
{
    public static string[] text;
    int index = 0;

	// Use this for initialization
	void Start (){
        if(text==null){
            text = new string[1];
            text[0] = "Haz Click";
        }
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
        if (text[index].Contains("\\n"))
        {
            text[index] = text[index].Replace("\\n","\n");
        }
        GUI.Box(new Rect(Screen.width * 0.4f,Screen.height * 0.2f,Screen.width*0.5f,Screen.height * 0.6f), text[index]);
    }
}
