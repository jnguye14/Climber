using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour
{
    [System.Serializable]
    public class Button
    {
        public Rect box;
        public string name;
        public string level;
    }

    public GUISkin skin;
    public Button[] buttons;

    public bool showQuit = true;
    public Rect quitButton = new Rect(0,0,100,50);

    // Use this for initialization
	void Start () { }
	
	// Update is called once per frame
	void Update () { }

	void OnGUI()
	{
        if (skin != null)
        {
            GUI.skin = skin;
        }

        // draw buttons
        foreach (Button b in buttons)
        {
            if (GUI.Button(b.box, b.name))
            {
                Application.LoadLevel(b.level);
            }
        }

        // quit button
        if (showQuit && GUI.Button(quitButton, "Quit"))
        {
            Application.Quit();
        }
	}
}