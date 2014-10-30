using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class presentBall : MonoBehaviour {

	//the ball objects to present to the player as the target
	public GameObject ball1;
	public GameObject ball2;
	public GameObject ball3;
	public GameObject ball4;
	public GameObject ball5;
	public GameObject ball6;
	public GameObject ball7;
	public GameObject ball8;
	GameObject[] balls = new GameObject[8];
	//a list of all the reaction times
	List<float> timeValues = new List<float>();
	//a list of all the balls that were hit
	List<int> correctHits = new List<int>();
	//checks if it is time to show the correct GUI
	bool showing = true;
	//the time in seconds
	float time;
	//the target value
	int theBall;
	//the instance of the ball generator
	public GameObject generator;
	//the instance of the script that trial settings is stored
	Settings script;
	//checks if it is in trial
	bool inTrial;
	//the number of trials
	int trials;
	//boolean to determine when to show results
	bool showResults;
	//the number of correct balls that went in
	int wentIns;
	//the number of correct target balls missed
	int missed;
	
	
	// Use this for initialization
	void Start () {
		
		balls[0] = ball1;
		balls[1] = ball2;
		balls[2] = ball3;
		balls[3] = ball4;
		balls[4] = ball5;
		balls[5] = ball6;
		balls[6] = ball7;
		balls[7] = ball8;
		
		script = GetComponent<Settings>();
		inTrial = false;
		trials = script.getTrials();
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		//after the first 2 seconds, stop showing target ball
		if (time > 2)
			showing = false;
		
		//if still in trial and is not last trial, continue trials
		if (trials > 0 && !inTrial) {
			beginTrial();
			trials --;
			inTrial = true;
		}
		//check if trial is over
		inTrial = !generator.GetComponent<createBall>().complete();
		
		//if all trials are done, show results
		if (trials == 0 && !inTrial)
			showResults = true;
	}
	
	//main function that starts a trial
	void beginTrial() {
		time = 0;
		showing = true;
		int prev = theBall;
		//while loop that prevents the same ball from being rolled twice in a row
		while (prev == theBall) {
			theBall = (int) Random.Range (0f, 8f);
		}
		GameObject ball = Instantiate (balls[theBall], new Vector3(transform.position.x+.3f, transform.position.y-0.2f,
		                                                           transform.position.z + 2.1f), new Quaternion(0,25,4,-1)) as GameObject;
		ball.SetActive (true);
		generator.SetActive (true);
		generator.GetComponent<createBall>().setTheBall (theBall);
	}
	
	//add a reaction time that the player hit space
	public void addTime (float t) {
		timeValues.Add (t);
	}
	
	//if this ball was the target ball, add a correct hit, otherwise, add a wrong hit
	public void checkBall (int v) {
		if (theBall+1 == (v))
			correctHits.Add (1);
		else 
			correctHits.Add (0);
	}
	
	//returns the average reaction time of all times stored in the list
	float getAverageTime() {
		float avg = 0;
		for (int i = 0; i < timeValues.ToArray().Length; i++) {
			avg += timeValues[i];
		}
		avg = avg / timeValues.ToArray ().Length;
		return avg;
	}
	
	//returns the number of correct hits
	int getCorrect() {
		int howmany = 0;
		for (int i = 0; i < correctHits.ToArray ().Length; i++) {
			if (correctHits[i] == 1)
				howmany ++;
		}
		return howmany;
	}
	
	//returns the number of target balls that were missed
	int getMissed() {
		return missed;
	}
	
	//add a score if a target ball went in
	public void addScore(int i) {
		if (i == theBall + 1)
			wentIns ++;
	}
	
	//add a miss if a target ball was not hit
	public void missCheck(int i) {
		if (i == theBall + 1)
			missed ++;
	}
	
	//shows target ball and the results screen
	void OnGUI () {
		if (showing) {
			GUI.skin.label.fontSize = Screen.width/20;
			GUI.Label (new Rect (Screen.width/2-Screen.width/10, Screen.height/3-Screen.height/4, Screen.width/2+Screen.width/10, Screen.height/2-Screen.height/6), "Target Ball: ");
			
			GUI.Label (new Rect (Screen.width/2-Screen.width/4, Screen.height/2+Screen.height/3, Screen.width/2+Screen.width/8, Screen.height/2+Screen.height/2), "Hit Space when you see it!");
		}
		
		if (showResults) {
			float percent = 0;
			//the GUI text. the size and position of text and buttons is relative to the size of the screen
			GUI.skin.label.fontSize = Screen.width/30;
			GUI.Label (new Rect (Screen.width / 50, Screen.height / 7, Screen.width, Screen.height), "Your Final Results:");
			GUI.Label (new Rect (Screen.width / 40, Screen.height / 4 + Screen.height / 20, Screen.width, Screen.height), "Average Reaction Time: " + getAverageTime().ToString() + " seconds");
			GUI.Label (new Rect (Screen.width / 40, Screen.height / 4 + (2 * Screen.height / 20) , Screen.width, Screen.height), "Number of Correct Hits: " + getCorrect().ToString() + " out of " + correctHits.ToArray ().Length.ToString());
			if (correctHits.ToArray ().Length == 0)
				GUI.Label (new Rect (Screen.width / 40, Screen.height / 4 + (3 * Screen.height / 20), Screen.width, Screen.height), "Accuracy: None!");
			else {
				percent = (float) (getCorrect() / (float)correctHits.ToArray ().Length) * 100;
				GUI.Label (new Rect (Screen.width / 40, Screen.height / 4 + (3 * Screen.height / 20), Screen.width, Screen.height), "Accuracy: " + percent.ToString() + "%");
			}
			GUI.Label (new Rect (Screen.width / 40, Screen.height / 4 + (4 * Screen.height / 20), Screen.width, Screen.height), "Missed: " + getMissed().ToString());
			GUI.Label (new Rect (Screen.width / 40, Screen.height / 4 + (5 * Screen.height / 20), Screen.width, Screen.height), "Bonus! Balls Went In: " + wentIns.ToString());
			
			//calculates the final score.
			//the faster the ball was hit out of 2 seconds, the better score. 100 points if player took an average of 0 seconds to hit every ball
			//every correct ball hit is 100 points
			//every incorrect ball hit is -100 points
			//the accuracy ratio of correct to incorrect is worth 100. 0 incorrect hits is 100 points
			//every missed target ball is -100 points
			//every target ball that goes into the hole is a bonus 100 points
			float final = ((2-getAverageTime())*100 + getCorrect()*100 - (correctHits.ToArray ().Length-getCorrect ())*100 + percent - getMissed()*100 + wentIns * 100);
			GUI.Label (new Rect (Screen.width / 40, Screen.height / 4 + (7 * Screen.height / 20), Screen.width, Screen.height), "Final Score: " + final.ToString());
			
		}
	}
}
