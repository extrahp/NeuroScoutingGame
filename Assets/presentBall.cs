using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class presentBall : MonoBehaviour {

	public GameObject ball1;
	public GameObject ball2;
	public GameObject ball3;
	public GameObject ball4;
	public GameObject ball5;
	public GameObject ball6;
	public GameObject ball7;
	public GameObject ball8;
	GameObject[] balls = new GameObject[8];
	List<float> timeValues = new List<float>();
	List<int> correctHits = new List<int>();
	bool showing = true;
	float time;
	int theBall;
	public GameObject generator;
	Settings script;
	bool inTrial;
	int trials;
	bool showResults;
	int wentIns;
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
		if (time > 2)
			showing = false;
		
		if (trials > 0 && !inTrial) {
			beginTrial();
			trials --;
			inTrial = true;
		}
		inTrial = !generator.GetComponent<createBall>().complete();
		
		if (trials == 0 && !inTrial)
			showResults = true;
	}
	
	void beginTrial() {
		time = 0;
		showing = true;
		int prev = theBall;
		while (prev == theBall) {
			theBall = (int) Random.Range (0f, 8f);
		}
		GameObject ball = Instantiate (balls[theBall], new Vector3(transform.position.x+.3f, transform.position.y-0.2f,
		                                                           transform.position.z + 2.1f), new Quaternion(0,25,4,-1)) as GameObject;
		ball.SetActive (true);
		generator.SetActive (true);
		generator.GetComponent<createBall>().setTheBall (theBall);
	}
	
	public void addTime (float t) {
		timeValues.Add (t);
		Debug.Log (t);
	}
	public void checkBall (int v) {
		Debug.Log (theBall+1);
		Debug.Log (v);
		if (theBall+1 == (v))
			correctHits.Add (1);
		else 
			correctHits.Add (0);
	}
	float getAverageTime() {
		float avg = 0;
		for (int i = 0; i < timeValues.ToArray().Length; i++) {
			avg += timeValues[i];
		}
		avg = avg / timeValues.ToArray ().Length;
		return avg;
	}
	int getCorrect() {
		int howmany = 0;
		for (int i = 0; i < correctHits.ToArray ().Length; i++) {
			if (correctHits[i] == 1)
				howmany ++;
		}
		return howmany;
	}
	public void addScore(int i) {
		if (i == theBall + 1)
			wentIns ++;
	}
	
	void OnGUI () {
		if (showing) {
			GUI.skin.label.fontSize = Screen.width/20;
			GUI.Label (new Rect (Screen.width/2-Screen.width/10, Screen.height/3-Screen.height/4, Screen.width/2+Screen.width/10, Screen.height/2-Screen.height/6), "Target Ball: ");
			
			GUI.Label (new Rect (Screen.width/2-Screen.width/4, Screen.height/2+Screen.height/3, Screen.width/2+Screen.width/8, Screen.height/2+Screen.height/2), "Hit Space when you see it!");
		}
		
		if (showResults) {
			GUI.skin.label.fontSize = Screen.width/30;
			GUI.Label (new Rect (Screen.width / 50, Screen.height / 7, Screen.width, Screen.height), "Your Final Results:");
			GUI.Label (new Rect (Screen.width / 40, Screen.height / 4 + Screen.height / 20, Screen.width, Screen.height), "Average Reaction Time: " + getAverageTime().ToString() + " seconds");
			GUI.Label (new Rect (Screen.width / 40, Screen.height / 4 + Screen.height / 10, Screen.width, Screen.height), "Number of Correct Hits: " + getCorrect().ToString() + " out of " + correctHits.ToArray ().Length.ToString());
			if (correctHits.ToArray ().Length == 0)
				GUI.Label (new Rect (Screen.width / 40, Screen.height / 4 + Screen.height / 5, Screen.width, Screen.height), "Accuracy: None!");
			else {
				float percent;
				percent = (float) getCorrect() / (float)correctHits.ToArray ().Length;
				GUI.Label (new Rect (Screen.width / 40, Screen.height / 4 + Screen.height / 5, Screen.width, Screen.height), "Accuracy: " + percent.ToString() + "%");
			}
			GUI.Label (new Rect (Screen.width / 40, Screen.height / 4 + Screen.height / 2, Screen.width, Screen.height), "Bonus! Balls Went In: " + wentIns.ToString());
			
		}
	}
}
