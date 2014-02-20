using UnityEngine;
using System.Collections;

public class TitleMenu : MonoBehaviour
{
	private int mode = 0;
	// 0 = title
	// 1 = credits
	// 2 = manual

	// Use this for initialization
	void Start () { mode = 0; }
	
	// Update is called once per frame
	void Update () { }

	void OnGUI()
	{
		switch (mode)
		{
		case 0:
			guiText.enabled = true;
			Rect buttonBox = new Rect (Screen.width * 0.5f, 0.0f, Screen.width * 0.2f, Screen.height * 0.2f);
			if (GUI.Button (buttonBox, "Tutorial"))
			{
				PlayerPrefs.SetInt("GameMode",0); // tutorial
				Application.LoadLevel("Room");
			}
			
			buttonBox = new Rect (Screen.width * 0.5f, Screen.height * 0.2f, Screen.width * 0.2f, Screen.height * 0.2f);
			if (GUI.Button (buttonBox, "Free Climb"))
			{
				PlayerPrefs.SetInt("GameMode",1); // free climb
				Application.LoadLevel("Room");
			}
			
			buttonBox = new Rect (Screen.width * 0.5f, Screen.height * 0.4f, Screen.width * 0.2f, Screen.height * 0.2f);
			if (GUI.Button (buttonBox, "Time Attack"))
			{
				PlayerPrefs.SetInt("GameMode",2); // tutorial
				Application.LoadLevel("Room");
			}
			
			buttonBox = new Rect (Screen.width * 0.5f, Screen.height * 0.6f, Screen.width * 0.2f, Screen.height * 0.2f);
			if (GUI.Button (buttonBox, "Credits"))
			{
				Debug.Log("you pressed me");
				mode = 1;
			}
			
			buttonBox = new Rect (Screen.width * 0.5f, Screen.height * 0.8f, Screen.width * 0.2f, Screen.height * 0.2f);
			if (GUI.Button (buttonBox, "Manual"))
			{
				Debug.Log("you pressed me");
				mode = 2;
			}
			break;
		case 1:
			guiText.enabled = true;
			guiText.text = "Credits!\n" +
					"Andrew Craze\n" +
					"Jordan Nguyen\n" +
					"Joseph Spiro\n" +
					"\nCSE598 - Design for Learning in Virtual Worlds\n" +
					"Spring 2014\n" +
					"Prof. Brian Nelson";
			guiText.fontSize = 20;

			Rect buttonBox1 = new Rect (Screen.width * 0.5f, 0.0f, Screen.width * 0.2f, Screen.height * 0.2f);
			if (GUI.Button (buttonBox1, "Back"))
			{
				mode = 0;
				guiText.text = "Monkey Mountain";
				guiText.fontSize = 50;
			}
			break;
		case 2:
			guiText.enabled = false;
			Rect buttonBox2 = new Rect (Screen.width * 0.5f, 0.0f, Screen.width * 0.2f, Screen.height * 0.2f);
			if (GUI.Button (buttonBox2, "Back"))
			{
				mode = 0;
			}
			break;
		default:
			break;
		}



	}
}
