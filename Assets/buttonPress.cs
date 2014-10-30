using UnityEngine;
using System.Collections;

public class buttonPress : MonoBehaviour {
	
	//can player hit space
	bool canHit;
	//time that has passed
	float time;
	//the sound for hitting
	public AudioClip clip;
	//the value or number of the pool ball
	public int value;
	//the object to return data to
	public GameObject player;

	// Use this for initialization
	void Start () {
		canHit = true;
		//start rolling when initialized
		rigidbody.AddForce (2500, 0, 0);
	}
	
	//returns the value of this ball
	public int getValue() {
		return value;
	}
	// Update is called once per frame
	void Update () {
	
		//time in seconds
		time += Time.deltaTime;
		//if 2 seconds has passed, player can not hit anymore
		if (time > 2)
			canHit = false;
			
		//hit the ball within 2 seconds
		if (Input.GetKeyDown ("space") && canHit) {
			rigidbody.AddForce (0, 0, 19000);
			canHit = false;
			AudioSource.PlayClipAtPoint(clip, transform.position);
			//store the reaction time it took the player to hit it
			player.GetComponent<presentBall>().addTime(time);
			//store the number of the ball that was hit to check if it is correct target
			player.GetComponent<presentBall>().checkBall(value);
		}
		//if 3 seconds has passed, destroy object and store that the ball was missed
		if (time > 3) {
			player.GetComponent<presentBall>().missCheck(value);
			Destroy(gameObject);
		}
	}
}
