using UnityEngine;
using System.Collections;
using System;

public class StairScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
			for (int x = 0; x < 10; x++) {
                
				GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.parent = this.transform;
				//cube.AddComponent<Rigidbody>();
				cube.transform.rotation = this.gameObject.transform.rotation;
                cube.transform.localPosition = new Vector3(x, 0.75f + (float)(0.25f * x /* Math.Tan(this.gameObject.transform.rotation.z)*/), 0);
				cube.transform.localScale+= new Vector3(0,0,3);
				// y = 2*x
				//cube.transform.position += this.gameObject.transform.position;
				Debug.Log(this.gameObject.transform.rotation);
				
			}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
