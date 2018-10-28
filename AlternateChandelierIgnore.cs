using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternateChandelierIgnore : MonoBehaviour {


	public Collider fixtureCollider;
	// Use this for initialization
	void Start () 
	{
		fixtureCollider = GameObject.FindGameObjectWithTag ("fixture").GetComponent<Collider>();
		Physics.IgnoreCollision (fixtureCollider, GetComponent<Collider> ());

	}
}
