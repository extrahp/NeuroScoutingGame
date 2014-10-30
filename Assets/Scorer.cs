using UnityEngine;
using System.Collections;

public class Scorer : MonoBehaviour {

	public GameObject scores;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider col) {
		scores.GetComponent<presentBall>().addScore(col.gameObject.GetComponent<buttonPress>().getValue());
	}
}
