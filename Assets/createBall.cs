using UnityEngine;
using System.Collections;

public class createBall : MonoBehaviour {

	public GameObject ball1;
	public GameObject ball2;
	public GameObject ball3;
	public GameObject ball4;
	public GameObject ball5;
	public GameObject ball6;
	public GameObject ball7;
	public GameObject ball8;
	float time;
	int timeInt;
	bool canCreate;
	GameObject[] balls = new GameObject[8];
	int series = 6;
	bool doneTrial;
	int prev;
	
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
		time = 0;
		timeInt = 0;
		doneTrial = false;
	}

	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		timeInt = (int)time;
		int rand = (int) Random.Range (0f, 8f);
		while (prev == rand) {
			rand = (int) Random.Range (0f, 8f);
		}
		if (timeInt > 2 && timeInt % 3 == 0 && canCreate && series > 0) {
			GameObject ball = Instantiate (balls[rand], transform.position, transform.rotation) as GameObject;
			prev = rand;
			ball.SetActive (true);
			series -= 1;
			canCreate = false;
			time = 0;
			timeInt = 0;
		}
		if (!canCreate && time < 2)
			canCreate = true;
		if (series == 0 && timeInt > 3)
			doneTrial = true;
	}

	public void setTheBall(int i) {
		doneTrial = false;
		time = 0;
		timeInt = 0;
		canCreate = true;
		series = 6;
	}
	
	public bool complete () {
		return doneTrial;
	}
	//void OnGUI() {
	//	GUI.Label (new Rect(10, 10, 100, 20), time.ToString ());
	//}
}
