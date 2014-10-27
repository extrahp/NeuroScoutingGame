using UnityEngine;
using System.Collections;

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
	bool showing = true;
	float time;
	int theBall;
	public GameObject generator;

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

		int theBall = (int) Random.Range (0f, 8f);
		GameObject ball = Instantiate (balls[theBall], new Vector3(transform.position.x+.3f, transform.position.y-0.2f,
		                                                           transform.position.z + 2.1f), new Quaternion(0,25,4,-1)) as GameObject;
		ball.SetActive (true);
		generator.GetComponent<createBall>().setTheBall (theBall);
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time > 2)
			showing = false;
	}

	void OnGUI () {
		if (showing) {
			GUI.skin.label.fontSize = Screen.width/20;
			GUI.Label (new Rect (Screen.width/2-Screen.width/10, Screen.height/3-Screen.height/4, Screen.width/2+Screen.width/10, Screen.height/2-Screen.height/6), "Target Ball: ");
			
			GUI.Label (new Rect (Screen.width/2-Screen.width/4, Screen.height/2+Screen.height/3, Screen.width/2+Screen.width/8, Screen.height/2+Screen.height/2), "Hit Space when you see it!");
		}
	}
}
