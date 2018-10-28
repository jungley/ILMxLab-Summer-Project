using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pin_behavior : StoryInteractable {


	public GameObject eyestickR_01;
	public GameObject eyestickR_02;
	public GameObject eyestickL_01;
	public GameObject eyestickL_02;

	public Material greenEnable;
	public Material orignalEyeMat;


	public override void EnableInteraction ()
	{
		//Set Parent to activated
		base.EnableInteraction ();
		//turn on glow
		glowOffOn(true, "All");

		respawnDist = 1.0f;

	}


	void glowOffOn(bool offOn, string name)
	{
		if (offOn) 
		{
			eyestickR_01.GetComponent<Renderer> ().material = greenEnable;
			eyestickR_02.GetComponent<Renderer> ().material = greenEnable;
			eyestickL_01.GetComponent<Renderer> ().material = greenEnable;
			eyestickL_02.GetComponent<Renderer> ().material = greenEnable;
		}

		//restore the old materials
		else 
		{
			if (name == "eyeTarget_Right") {
				eyestickR_01.GetComponent<Renderer> ().material = orignalEyeMat;
				eyestickR_02.GetComponent<Renderer> ().material = orignalEyeMat;
			} 
			if (name == "eyeTarget_Left") {
				eyestickL_01.GetComponent<Renderer> ().material = orignalEyeMat;
				eyestickL_02.GetComponent<Renderer> ().material = orignalEyeMat;
			}
		}
	}


	//TODO Make sure pins freeze into the trigger area once they have been inserted
	//Had an idea for how to do this then I forgot =/


	//Insert pin into eye
	void OnTriggerEnter(Collider other)
	{
		//If pin hit the target
		if (other.tag == "eyeTarget_Left" || other.tag == "eyeTarget_Right") {

			//restore eye
			glowOffOn(false, other.tag);


			//Attach to the eye
			var joint = transform.parent.gameObject.AddComponent<FixedJoint> ();
			joint.connectedBody = other.attachedRigidbody;

			//Make eye unusable by other pins
			other.tag = "UsedEyeTarget";
			transform.parent.tag = "pin_inserted";
			//Add inserted pin count

			//make it it doesnt collide with anything anymore
			GetComponentInParent<Collider>().isTrigger = true;
			GetComponentInParent<Rigidbody> ().detectCollisions = false;

			EventManager.pins_in_eye_count++;

			//change rigidbody settings
			GetComponentInParent<Rigidbody>().mass = 0;
			GetComponentInParent<Rigidbody> ().angularDrag = 0;
			GetComponentInParent<Rigidbody> ().useGravity = true;

			//GetComponentInParent<Rigidbody> ().freezeRotation = true;
			//GetComponentInParent<Rigidbody>().

			gameObject.GetComponent<pin_behavior>().enabled = false;

			if (EventManager.pins_in_eye_count == 2) 
			{
				CompletedInteraction ();

			}

			//DISABLE PIN SCRIPT
			GetComponent<pin_behavior>().enabled = false;

        }
	}
		
}
