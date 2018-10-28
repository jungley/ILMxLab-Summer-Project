using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dollLimbsOffOn : MonoBehaviour {

	//TOTAL LAST WEEK HACK YOLO
	//Dis shiz is not being maintained so whatevs

	public GameObject leftArm;
	public GameObject rightArm;
	public GameObject leftLeg;
	public GameObject rightLeg;
	public GameObject body;

	float maxVel = 2.5f;




	void Update()
	{
		/*if (body.GetComponent<Rigidbody> ().velocity.magnitude >= maxVel) 
		{
			
		} */
	}

	//Completley unmaintainable
/*	void Update()
	{

		//LEFT ARM
		if (leftArm.GetComponent<Rigidbody> ().velocity.z > maxVel) {
			Vector3 v = leftArm.GetComponent<Rigidbody> ().velocity;
			v.z = maxVel;
			leftArm.GetComponent<Rigidbody> ().velocity = v;
		}
		if (leftArm.GetComponent<Rigidbody> ().velocity.x > maxVel) {
			Vector3 v = leftArm.GetComponent<Rigidbody> ().velocity;
			v.x = maxVel;
			leftArm.GetComponent<Rigidbody> ().velocity = v;
		}

		if (leftArm.GetComponent<Rigidbody> ().velocity.y > maxVel) {
			Vector3 v = leftArm.GetComponent<Rigidbody> ().velocity;
			v.y = maxVel;
			leftArm.GetComponent<Rigidbody> ().velocity = v;
		}



		//LEFT LEG
		if (leftLeg.GetComponent<Rigidbody> ().velocity.z > maxVel) {
			Vector3 v = leftLeg.GetComponent<Rigidbody> ().velocity;
			v.z = maxVel;
			leftLeg.GetComponent<Rigidbody> ().velocity = v;
		}
		if (leftLeg.GetComponent<Rigidbody> ().velocity.x > maxVel) {
			Vector3 v = leftLeg.GetComponent<Rigidbody> ().velocity;
			v.x = maxVel;
			leftLeg.GetComponent<Rigidbody> ().velocity = v;
		}

		if (leftLeg.GetComponent<Rigidbody> ().velocity.y > maxVel) {
			Vector3 v = leftLeg.GetComponent<Rigidbody> ().velocity;
			v.y = maxVel;
			leftLeg.GetComponent<Rigidbody> ().velocity = v;
		}


		//RIGHT ARM
		if (rightArm.GetComponent<Rigidbody> ().velocity.z > maxVel) {
			Vector3 v = rightArm.GetComponent<Rigidbody> ().velocity;
			v.z = maxVel;
			rightArm.GetComponent<Rigidbody> ().velocity = v;
		}
		if (rightArm.GetComponent<Rigidbody> ().velocity.x > maxVel) {
			Vector3 v = rightArm.GetComponent<Rigidbody> ().velocity;
			v.x = maxVel;
			rightArm.GetComponent<Rigidbody> ().velocity = v;
		}
		if (rightArm.GetComponent<Rigidbody> ().velocity.y > maxVel) {
			Vector3 v = rightArm.GetComponent<Rigidbody> ().velocity;
			v.y = maxVel;
			rightArm.GetComponent<Rigidbody> ().velocity = v;
		}


		//RIGHT LEG
		if (rightLeg.GetComponent<Rigidbody> ().velocity.z > maxVel) {
			Vector3 v = rightLeg.GetComponent<Rigidbody> ().velocity;
			v.z = maxVel;
			rightLeg.GetComponent<Rigidbody> ().velocity = v;
		}
		if (rightLeg.GetComponent<Rigidbody> ().velocity.x > maxVel) {
			Vector3 v = rightLeg.GetComponent<Rigidbody> ().velocity;
			v.x = maxVel;
			rightLeg.GetComponent<Rigidbody> ().velocity = v;
		}
		if (rightLeg.GetComponent<Rigidbody> ().velocity.y > maxVel) {
			Vector3 v = rightLeg.GetComponent<Rigidbody> ().velocity;
			v.y = maxVel;
			rightLeg.GetComponent<Rigidbody> ().velocity = v;
		}
	}



	void OnTriggerEnter(Collider other)
	{

		SteamVR_ControllerManager manager = GameObject.FindObjectOfType<SteamVR_ControllerManager> ();

		if (other.tag == "GameController" && (manager.left.GetComponent<ControllerGrabObject> ().isGrabbing
			|| manager.left.GetComponent<ControllerGrabObject> ().isGrabbing)) 
		{
			leftArm.GetComponent<Rigidbody> ().detectCollisions = false;
			rightArm.GetComponent<Rigidbody> ().detectCollisions = false;
			rightLeg.GetComponent<Rigidbody> ().detectCollisions = false;
			leftLeg.GetComponent<Rigidbody> ().detectCollisions = false;
		}
	}



	void OnTriggerExit(Collider other)
	{

		SteamVR_ControllerManager manager = GameObject.FindObjectOfType<SteamVR_ControllerManager> ();

		if (other.tag == "GameController" && (manager.left.GetComponent<ControllerGrabObject> ().isGrabbing
			|| manager.left.GetComponent<ControllerGrabObject> ().isGrabbing)) 
		{
			leftArm.GetComponent<Rigidbody> ().detectCollisions = true;
			rightArm.GetComponent<Rigidbody> ().detectCollisions = true;
			rightLeg.GetComponent<Rigidbody> ().detectCollisions = true;
			leftLeg.GetComponent<Rigidbody> ().detectCollisions = true;
		}

	}


*/
} 
