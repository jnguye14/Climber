using UnityEngine;
using System.Collections;

public class TriggerBoxScript : MonoBehaviour {
	bool boxSizeDebugOn;
	bool entranceDebugOn;
	// Use this for initialization
	void Start () {
		boxSizeDebugOn = false;
		entranceDebugOn = false;
		renderer.enabled = false;
		GameObject chi = transform.GetChild(0).gameObject;
		chi.renderer.enabled= false;
	}
	
	// Update is called once per frame
	void Update () {
		//renderer.enabled=entranceDebugOn;
		if (Input.GetKeyDown ("u")){
			boxSizeDebugOn = !boxSizeDebugOn;
		}
		if (Input.GetKeyDown ("i")){
			entranceDebugOn = !entranceDebugOn;
		}
		renderer.enabled = boxSizeDebugOn;
	}
	void OnTriggerEnter(Collider other) {
			GameObject chi = transform.GetChild(0).gameObject;
			chi.renderer.enabled= entranceDebugOn && true;
    }
	void OnTriggerExit(Collider other) {
			GameObject chi = transform.GetChild(0).gameObject;
			chi.renderer.enabled= false;
    }
}
