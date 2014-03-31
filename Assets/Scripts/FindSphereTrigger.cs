using UnityEngine;
using System.Collections;

public class FindSphereTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		string[] tags = { "Handhold", "Foothold", "Multihold", "Checkpoint", "Goal"};
		Color[] colors = {Color.blue, Color.cyan, Color.white, Color.green, Color.red};
		int i = 0;
		foreach(string tag in tags)
		{
			GameObject[] spheres = GameObject.FindGameObjectsWithTag (tag);

			foreach (GameObject s in spheres) {
					s.GetComponent<Collider> ().isTrigger = true;
				s.GetComponent<Renderer>().material.color = colors[i];
					
			}
			i++;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
