using UnityEngine;
using System.Collections;

public class Settings : MonoBehaviour {
	
	//has the player selected how many trials?
	bool selected;
	
	//number of trials
	int trials;

	// Use this for initialization
	void Start () {
		selected = false;
		trials = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
		//if player has selected number of trials, begin game
		if (selected) {
			GetComponent<presentBall>().enabled = true;
		}
	}
	
	//returns how many trials
	public int getTrials () {
		return trials;
	}
	
	//display the buttons and options for trials
	void OnGUI () {
		if (!selected) {
			GUI.skin.label.fontSize = Screen.width / 20;
			GUI.Label (new Rect (Screen.width / 5, Screen.height / 3 - Screen.height / 4, Screen.width / 2 + Screen.width / 10, Screen.height / 2 - Screen.height / 6), "Please select the number of trials.");
			for (int i = 0; i < 5; i++) {
				if (GUI.Button (new Rect(Screen.width / 20 + (i*Screen.width / 5), Screen.height / 2 - Screen.height / 20, Screen.height / 10 + Screen.height / 10, Screen.height / 20 + Screen.height / 20), (i+1).ToString ())) {
					trials = i + 1;
					selected = true;
				}
			}
		}
	}
}
