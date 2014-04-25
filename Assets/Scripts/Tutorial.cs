using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour
{
    public string[] text;
    int index = 0;
    private bool showText = true;

	// Use this for initialization
	void Start (){
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (showText)
        {
            if (Input.GetMouseButtonDown(0)) // left click
            {
                if (index < text.Length - 1)
                {
                    index++;
                }
                else
                {
                    //Application.LoadLevel("tiles");
                    index = 0;
                    showText = false;
                }
            }
        }
	}

    void OnGUI()
    {
        if (showText)
        {
            GUI.skin.box.alignment = TextAnchor.MiddleCenter;
            if (text[index].Contains("\\n"))
            {
                text[index] = text[index].Replace("\\n", "\n");
            }
            GUI.Box(new Rect(Screen.width * 0.4f, Screen.height * 0.2f, Screen.width * 0.5f, Screen.height * 0.6f), text[index]);
        }
    }

    public void ShowText()
    {
        showText = true;
    }

    public void HideText()
    {
        showText = false;
    }

    public bool isShowing()
    {
        return showText;
    }
}
