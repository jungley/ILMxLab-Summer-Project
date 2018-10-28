using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKnobJiggle : MonoBehaviour {


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

}