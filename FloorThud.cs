using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorThud : MonoBehaviour {



	void OnCollisionEnter(Collision col)
	{
		if(Time.timeSinceLevelLoad > 1.0)
		{
			AudioSource dropThud = GetComponent<AudioSource>();
			dropThud.PlayOneShot(dropThud.clip);
		}
	}
}
