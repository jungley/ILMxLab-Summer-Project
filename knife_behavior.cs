using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knife_behavior : StoryInteractable {

	bool hasSwiveled = false;
	float smooth = 2.0f;

	public GameObject bodystick_01;
	public GameObject bodystick_02;

	public GameObject[] knifeHandles;

	public Material knifeMat;

	public Material knife_greenEnable;
	public Material greenEnable;
	public Material originalBodyMat;


	public override void EnableInteraction ()
	{
		//Set Parent to activated
		base.EnableInteraction ();
		//turn on glow
		glowOffOn(true);
		respawnDist = 4.0f;

	}


	void glowOffOn(bool offOn)
	{

		if (offOn) {

            //get the material of the knife
			knifeMat = knifeHandles[0].GetComponent<Renderer>().material;

			bodystick_01.GetComponent<Renderer> ().material = greenEnable;
			bodystick_02.GetComponent<Renderer> ().material = greenEnable;

			for (int i = 0; i < knifeHandles.Length; i++) {
				knifeHandles [i].GetComponent<Renderer> ().material = knife_greenEnable;
			}
		}

		//restore the old materials
		else {
			bodystick_01.GetComponent<Renderer> ().material = originalBodyMat;
			bodystick_02.GetComponent<Renderer> ().material = originalBodyMat;

			for (int k = 0; k < knifeHandles.Length; k++) {
				knifeHandles [k].GetComponent<Renderer> ().material = knifeMat;
			}
		}
	} 


	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "bodyTarget") {

			//turn off Glow
			glowOffOn(false);
			var joint = transform.parent.gameObject.AddComponent<FixedJoint> ();
			joint.connectedBody = other.attachedRigidbody;
			transform.parent.tag = "knife_inserted";

			//turn off rigidbody
			//make it it doesnt collide with anything anymore
			GetComponentInParent<Collider>().isTrigger = true;
			GetComponentInParent<Rigidbody> ().detectCollisions = false;

			CompletedInteraction ();
			isComplete = true;

			//disable parent rigidbody
			GetComponentInParent<Rigidbody>().mass = 0;
			GetComponentInParent<Rigidbody> ().angularDrag = 0;
			GetComponentInParent<Rigidbody> ().useGravity = true;

			gameObject.GetComponent<knife_behavior> ().enabled = false;
		}


	}

}
