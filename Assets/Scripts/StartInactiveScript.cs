using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StartInactiveScript : MonoBehaviour {
	public static List<GameObject> DeadObjs;
	// Use this for initialization
	void Start () {
		if (DeadObjs == null) DeadObjs= new List<GameObject>();
		DeadObjs.Add(this.gameObject);
		this.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

