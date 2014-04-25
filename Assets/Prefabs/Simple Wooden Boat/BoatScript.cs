using UnityEngine;
using System.Collections;

public class BoatScript : MonoBehaviour
{
    float speed = 10.0f;
    Vector3 beachedLocation = new Vector3(197.0f, 4.0f, 195.0f);//new Vector3(216, 5, 195);

	// Update is called once per frame
	void Update ()
    {
        // move to beached location
        this.transform.position = Vector3.MoveTowards(this.transform.position, beachedLocation, speed * Time.deltaTime);
	}
}
