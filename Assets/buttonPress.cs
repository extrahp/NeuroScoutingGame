using UnityEngine;
using System.Collections;

public class buttonPress : MonoBehaviour {

	bool canHit;
	float time;
	public AudioClip clip;

	// Use this for initialization
	void Start () {
		canHit = true;
		rigidbody.AddForce (2500, 0, 0);
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
		}
		if (time > 3) {
			Destroy(gameObject);
		}
	}
}
