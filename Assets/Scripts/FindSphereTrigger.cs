using UnityEngine;
using System.Collections;

public class FindSphereTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		string[] tags = { "Handhold", "Foothold", "Multihold", "Checkpoint", "Goal"};

		foreach(string tag in tags)
		{
			GameObject[] spheres = GameObject.FindGameObjectsWithTag (tag);

			foreach (GameObject s in spheres) {
					s.GetComponent<Collider> ().isTrigger = true;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
