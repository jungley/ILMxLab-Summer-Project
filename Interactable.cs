using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

	// Use this for initialization
	protected Vector3 originalSpot;
	protected Quaternion originalRot;
	public float respawnDist = 3.0f;

	private SteamVR_ControllerManager manager;


	void Awake()
	{
		originalSpot = transform.parent.position;
		originalRot = transform.parent.rotation;

		manager = GameObject.FindObjectOfType<SteamVR_ControllerManager> ();

	}
	
	// Update is called once per frame
	protected virtual void Update () {


		if (!(manager.left.GetComponent<ControllerGrabObject> ().isGrabbing ||
			manager.right.GetComponent<ControllerGrabObject> ().isGrabbing)) {

			//If reaches out of bound of the play area return to original position
			if (Vector3.Distance (originalSpot, transform.parent.position) > respawnDist) {
				respawn (originalSpot.x, originalSpot.y, originalSpot.z);
			}


			if (originalSpot.y - transform.parent.position.y > 0.4f) {
				respawn (originalSpot.x, originalSpot.y, originalSpot.z);
			}
		}
	}



	protected virtual void respawn(float xPos, float yPos, float zPos)
	{
		
		//zero out velocity
		transform.parent.GetComponent<Rigidbody>().velocity = Vector3.zero;
		transform.parent.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		//back to original spot
		transform.parent.position = new Vector3(xPos, yPos, zPos);
		transform.parent.rotation = originalRot;
	}
}
