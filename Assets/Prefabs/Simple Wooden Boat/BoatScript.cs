using UnityEngine;
using System.Collections;

public class BoatScript : MonoBehaviour
{
    float speed = 10.0f;
    bool finished;
    CharacterMotor mot;
    Vector3 beachedLocation = new Vector3(197.0f, 4.0f, 195.0f);//new Vector3(216, 5, 195);

    void Start(){
    	finished = false;
    	mot = this.transform.GetChild(0).GetComponent<CharacterMotor>();
    	mot.enabled=false;
    }
	// Update is called once per frame
	void Update ()
    {
        // move to beached location
        this.transform.position = Vector3.MoveTowards(this.transform.position, beachedLocation, speed * Time.deltaTime);

        if(!finished){
        	if(this.transform.position==beachedLocation){
        		finished =true;
        		this.transform.DetachChildren();
        		//Debug.Log("Children have been deatched.");
        		mot.enabled=true;
        	} 
        }

	}
}
