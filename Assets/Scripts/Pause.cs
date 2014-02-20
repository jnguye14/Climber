using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

	private bool isPaused = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			isPaused = !isPaused;	
		}
	}

	void OnGUI()
	{
		if (isPaused)
		{
			Rect box = new Rect(0.0f,0.0f,Screen.width,Screen.height);
			GUI.Label (box,"Paused");
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}
	}
}
