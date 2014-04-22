using UnityEngine;
using System.Collections;

public class CameraLock : MonoBehaviour
{
    public GameObject objToLookAt;
    public float distance = 10.0f;
	
	// Update is called once per frame
	void Update ()
    {
        if (objToLookAt != null)
        {
            Vector3 pos = objToLookAt.transform.position;
            pos.z += -distance;
            this.transform.position = pos;
        }
	}

    void LockCameraTo(GameObject obj)
    {
        objToLookAt = obj;
    }

    void UnLock()
    {
        objToLookAt = null;
    }
}
