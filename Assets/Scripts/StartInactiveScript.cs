using UnityEngine;
using System.Collections;

public class StartInactiveScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		/*
		MonoBehaviour[] comps = this.gameObject.GetComponents<MonoBehaviour>();
		 
		foreach(MonoBehaviour c in comps)
		{
		  c.enabled = false;
		}
		*/
		this.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

