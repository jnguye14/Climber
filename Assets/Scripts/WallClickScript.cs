using UnityEngine;
using System.Collections;

public class WallClickScript : MonoBehaviour {
	public GameObject fpc;
	// Use this for initialization
	void Start () {
		fpc = GameObject.FindGameObjectsWithTag("FPC")[0];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnMouseDown() {
		GameObject clim;
		GameObject cam;
		fpc = GameObject.FindGameObjectsWithTag("FPC")[0];
        fpc.SetActive(false);
        for(int i = 0; i< StartInactiveScript.DeadObjs.Count; i++)
        StartInactiveScript.DeadObjs[i].SetActive(true);
        //create one instead
        //clim = Instantiate(Root, new Vector3(185.4f, 24.0f, 272.0f), Quaternion.identity);
        //185.4704 24.46869 272.6973
        /*
        clim = GameObject.FindGameObjectsWithTag("Climber")[0];
        MonoBehaviour[] comps = clim.GetComponents<MonoBehaviour>();
        foreach(MonoBehaviour c in comps){
		  c.enabled = true;
		}*/
		/*
		clim = TurnOffAllComps.climber;
		clim.SetActive(true);
 
        cam = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        */
        //CameraLock scrip = cam.GetComponent(CameraLock);
        //scrip.LockCameraTo(clim);

        /*
        						- turn off fpc
						- turn on the model
						- turn on climb camera and enable camera lock giving it model as object
							- maybe specify its over th shoulder
        */

        /*
            void LockCameraTo(GameObject obj)
    {
        objToLookAt = obj;
    }
        */
    }
}
