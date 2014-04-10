using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class WallScript : MonoBehaviour {
	int numTries;
	List<GameObject> allNodes;
	//List<GameObject> perfPath;
	List<GameObject> pathTaken;
	//
	List<GameObject> errorReport;
	// I noted the specific objects and not just their error text so
	// denoting paths and errors later will be easier.
	List<string> errRepTxt;
	// Use this for initialization
	void Start () {
		numTries = 3;
		allNodes = new List<GameObject>();
		for(int i = 0; i < this.gameObject.transform.childCount; i++){
			 allNodes.Add( this.gameObject.transform.GetChild(i).gameObject);
		}
		//

		//
		pathTaken = new List<GameObject>();
		errorReport = new List<GameObject>();
		errRepTxt = new List<string>();
	}
	public void addNodeToPathTaken(GameObject newNode){
		if((pathTaken.Count==0)||(pathTaken[pathTaken.Count-1]!=newNode)){
			pathTaken.Add(newNode);
			//print ("it's been added");
		}
		if((newNode.GetComponent<TriggerBoxScript>().isErrNode)&& !errorReport.Contains(newNode)){
			errorReport.Add(newNode);
			//print("one of the error nodes was reached. position is " + errorReport.Count);
			print(newNode.GetComponent<TriggerBoxScript>().errNodeFeedback + " position is " + errorReport.Count);
			errRepTxt.Add(newNode.GetComponent<TriggerBoxScript>().errNodeFeedback);
		}
		if(newNode.GetComponent<TriggerBoxScript>().isEndNode){
			print("You've finished.");
			endCourse(true);
		}

	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("f")){
			decrLives();
		}
		if (Input.GetKeyDown ("w")){
			endCourse(true);
		}
	}

	void decrLives(){
		--numTries;
		if(numTries<1) endCourse(false);
	}
	void endCourse(bool hadWon){
		List<string> fedB = generateFeedback(hadWon);
		for(int c = 0; c < fedB.Count ; c++)print(fedB[c]);
		FeedbackScreenScript.text = fedB.ToArray();
		Application.LoadLevel("FeedbackScreen");
	}
	List<string> generateFeedback(bool hadWon){
		List<string> outgoing= new List<string>();
		if(hadWon){
			outgoing.Add("Congratualations on completing the course!");
		}else{
			outgoing.Add("Ahh sadly you've failed to summit the wall but let's recap what you could have done differently");
		}

		if(errRepTxt.Count>0){
			outgoing.Add("Here's some feedback about your climb.");
			for(int c = 0; c<errRepTxt.Count; c++) outgoing.Add(errRepTxt[c]);
		}else{
			outgoing.Add("It seems as though you made no mistakes! Congratualations!");
		}

		return outgoing;
	}
}
