using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//BASICALLY A SPECIAL VERSION OF INTERACTABLE
//BUT RESPAWNS THE DOLL
[System.Serializable]
public class doll_behavior : MonoBehaviour
{
	// Use this for initialization
	protected Vector3 originalSpot;
	protected Quaternion originalRot;

	protected Vector3 originalSpot_leftArm;
	protected Quaternion originalRot_leftArm;

	protected Vector3 originalSpot_rightArm;
	protected Quaternion originalRot_rightArm;

	protected Vector3 originalSpot_leftLeg;
	protected Quaternion originalRot_leftLeg;

	protected Vector3 originalSpot_rightLeg;
	protected Quaternion originalRot_rightLeg;


	public GameObject leftArm;
	public GameObject rightArm;
	public GameObject rightLeg;
	public GameObject leftLeg;

	public bool updateLock = false;

		void Awake()
		{
			//Get body transform/rotation
			originalSpot = transform.position;
			originalRot = transform.rotation;

			//get leftArm transform/rotation
		    originalSpot_leftArm = leftArm.transform.position;
		    originalRot_leftArm = leftArm.transform.rotation;

			//get rightArm transform/rotation
			originalSpot_rightArm = rightArm.transform.position;
			originalRot_rightArm = rightArm.transform.rotation;

			//get leftLeg transform/rotation
			originalSpot_leftLeg = leftLeg.transform.position;
			originalRot_leftLeg = leftLeg.transform.rotation;

			//get rightLeg transform/rotation
			originalSpot_rightLeg = rightLeg.transform.position;
			originalRot_rightLeg = rightLeg.transform.rotation;
		}

		// Update is called once per frame
		protected void Update () 
	    {
			//If reaches out of bound of the play area return to original position
			if (Vector3.Distance (originalSpot, transform.position) > 10.0f) 
			{
				respawnAll ();
			}
		  
		}

	public void respawnAll()
	{
		//body
		respawn (this.gameObject, originalSpot, originalRot);
		respawn (leftArm, originalSpot_leftArm, originalRot_leftArm);
		respawn (rightArm, originalSpot_rightArm, originalRot_rightArm);
		respawn (leftLeg, originalSpot_leftLeg, originalRot_leftLeg);
		respawn (rightLeg, originalSpot_rightLeg, originalRot_rightLeg);
	}


	protected void respawn(GameObject bodyPart, Vector3 spot, Quaternion rot)
		{
			//If it is a limb - disconnect it
			if (bodyPart.GetComponent<CharacterJoint> ()) 
			{
				bodyPart.GetComponent<CharacterJoint> ().connectedBody = null;
			}

			//zero out velocity on body
			transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
			transform.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
			//back to original spot
			bodyPart.transform.position = spot;
			bodyPart.transform.rotation = rot;

			//if it is a limb - reconnect it
			if (bodyPart.GetComponent<CharacterJoint> ()) 
			{
				bodyPart.GetComponent<CharacterJoint> ().connectedBody = this.gameObject.GetComponent<Rigidbody>();
			}
	   }
}
