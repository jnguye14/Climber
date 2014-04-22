using UnityEngine;
using System.Collections;

public class RotateAround : MonoBehaviour
{
    public GameObject objToLookAt;
    public float distance = -1.0f; // units away from object
    public float speed = 1.0f;
    private float theta = 0.0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
    {
        theta += Time.deltaTime * speed;
        if (theta > 2 * Mathf.PI)
        {
            theta -= 2 * Mathf.PI;
        }
        Vector3 direction = new Vector3(Mathf.Cos(theta), -0.5f, Mathf.Sin(theta));
        Debug.Log(theta);
        this.transform.Rotate(Vector3.up, - Time.deltaTime*Mathf.Rad2Deg*speed);
        this.transform.position = objToLookAt.transform.position + direction * distance;

        if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
        {
            distance -= 0.5f;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && distance < 0) // forward
        {
            distance += 0.5f;
        }
	}
}
