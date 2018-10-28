using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandFading : MonoBehaviour {

	public bool handHasFaded = false;
	public Material handMat;
	MeshRenderer handModel;

	// Use this for initialization
	void Start () 
	{
		//Set Alpha to 1
		handModel = GetComponent<MeshRenderer> ();
		Color color = handMat.color;
		color.a = 1;
		handMat.color = color;
	}
	
	// Update is called once per frame
	void Update () {

		//DISAPPEAR HAND
		if (GetComponentInParent<ControllerGrabObject> ().isGrabbing && !handHasFaded) {
			handHasFaded = true;
			StopAllCoroutines ();
			//change to transparent
			//set to transparent
			StartCoroutine ("AlphaFade", true);
		} 
		//MAKE HAND APPEAR
		else if (!GetComponentInParent<ControllerGrabObject> ().isGrabbing && handHasFaded) {
			handHasFaded = false;
			StopAllCoroutines ();
			StartCoroutine ("AlphaFade", false);
			handModel.enabled = true;
		}
	}

	IEnumerator AlphaFade(bool fadeOut)
	{
		if (fadeOut) 
		{
			
			while (handMat.color.a != 0 && handMat.color.a > 0) 
			{
				Color color = handMat.color;
				color.a -= 0.03f;
				handMat.color = color;
				if(handMat.color.a < 0.0001)
				{
					handModel.enabled = false;
				}
				yield return null;
			}
		} 
		else 
		{
			while (handMat.color.a != 1 && handMat.color.a < 1) 
			{
				Color color = handMat.color;
				color.a += 0.01f;
				handMat.color = color;
				if (handMat.color.a > 0.99) 
				{
					//enable mesh renderer
					handModel.enabled = true;
				}

				yield return null;
			}
		 }
	 }
}
