using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour
{
	public Vector3 TutorialSpawnLoc = new Vector3(5,2,-5);
	public Vector3 FreeClimbSpawnLoc = new Vector3(-5,2,-5);
	public Vector3 TimeAttackSpawnLoc = new Vector3(-5,2,5);

	// Use this for initialization
	void Start () {
		switch(PlayerPrefs.GetInt("GameMode"))
		{
		case 0: // tutorial
			this.transform.position = TutorialSpawnLoc;
			break;
		case 1: // free climb
			this.transform.position = FreeClimbSpawnLoc;
			break;
		case 2: // time attack
			this.transform.position = TimeAttackSpawnLoc;
			break;
		default:
			this.transform.position = Vector3.zero + new Vector3(0,2,0);
			break;
		}
	}
	
	/*// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Application.LoadLevel("Title");
		}
	}//*/
}
