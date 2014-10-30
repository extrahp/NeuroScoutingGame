using UnityEngine;
using System.Collections;

public class buttonPress : MonoBehaviour {

	bool canHit;
	float time;
	public AudioClip clip;
	public int value;
	public GameObject player;

	// Use this for initialization
	void Start () {
		canHit = true;
		rigidbody.AddForce (2500, 0, 0);
	}
	public int getValue() {
		return value;
	}
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time > 2)
			canHit = false;
		if (Input.GetKeyDown ("space") && canHit) {
			rigidbody.AddForce (0, 0, 19000);
			canHit = false;
			AudioSource.PlayClipAtPoint(clip, transform.position);
			player.GetComponent<presentBall>().addTime(time);
			player.GetComponent<presentBall>().checkBall(value);
		}
		if (time > 3) {
			player.GetComponent<presentBall>().missCheck(value);
			Destroy(gameObject);
		}
	}
}
