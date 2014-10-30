using UnityEngine;
using System.Collections;

public class createBall : MonoBehaviour {
	
	//the list of balls
	public GameObject ball1;
	public GameObject ball2;
	public GameObject ball3;
	public GameObject ball4;
	public GameObject ball5;
	public GameObject ball6;
	public GameObject ball7;
	public GameObject ball8;
	
	//the time that passes
	float time;
	//the time in integers
	int timeInt;
	//check if can create next ball
	bool canCreate;
	//an array of balls
	GameObject[] balls = new GameObject[8];
	//how many to create in a trial
	int series = 6;
	//has this trial completed
	bool doneTrial;
	//the previous ball that was created
	int prev;
	
	// Use this for initialization
	void Start () {
		//store the ball instances into an array
		balls[0] = ball1;
		balls[1] = ball2;
		balls[2] = ball3;
		balls[3] = ball4;
		balls[4] = ball5;
		balls[5] = ball6;
		balls[6] = ball7;
		balls[7] = ball8;
		time = 0;
		timeInt = 0;
		doneTrial = false;
	}

	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		timeInt = (int)time;
		//prevent the same ball from being the target multiple times in a row
		int rand = (int) Random.Range (0f, 8f);
		while (prev == rand) {
			rand = (int) Random.Range (0f, 8f);
		}
		
		//if 3 seconds has passed, and the series of 6 is not over, create next ball
		if (timeInt > 2 && timeInt % 3 == 0 && canCreate && series > 0) {
			GameObject ball = Instantiate (balls[rand], transform.position, transform.rotation) as GameObject;
			prev = rand;
			ball.SetActive (true);
			series -= 1;
			canCreate = false;
			time = 0;
			timeInt = 0;
		}
		//prepare to create next ball
		if (!canCreate && time < 2)
			canCreate = true;
			
		//if 6 balls were created, the trial is done
		if (series == 0 && timeInt > 3)
			doneTrial = true;
	}

	//set the target ball value
	public void setTheBall(int i) {
		doneTrial = false;
		time = 0;
		timeInt = 0;
		canCreate = true;
		series = 6;
	}
	
	//check if the trial is done
	public bool complete () {
		return doneTrial;
	}
}
