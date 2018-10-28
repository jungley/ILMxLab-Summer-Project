using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeRaycast : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 fwd = transform.TransformDirection (Vector3.forward);
		RaycastHit hit;

		//pew pew Always shooting out rays
		if (Physics.Raycast (transform.position, fwd, out hit, 100)) 
		{
			Debug.DrawRay (transform.position, fwd, Color.green);

			//Trigger Animations
			if (hit.collider.tag == "AnimationTrigger") 
			{
				hit.collider.GetComponentInParent<AnimationManager> ().PlayAnimation ();
				hit.collider.tag = "Finish";
			}

		}
	}
}
