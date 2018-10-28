using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chandelierTrigger : MonoBehaviour {


	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "GameController") {
			other.gameObject.GetComponent<Collider> ().isTrigger = false;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "GameController") {
			other.gameObject.GetComponent<Collider> ().isTrigger = true;
		}
	}

	void Update()
	{
		if (EventManager.shakeChandelier) {
			GetComponent<Rigidbody> ().AddForce (600, 0, 0);
			EventManager.shakeChandelier = false;
		}
	}

}
