using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowySinWave : MonoBehaviour {

	public Material glowyArea;
	private Color originalCol;


	// Use this for initialization
	void Start () {
		//glowyArea = this.GetComponent<MeshRenderer> ().sharedMaterial;
		MeshRenderer temp = this.GetComponent<MeshRenderer> ();
		temp.material.SetFloat ("nothing", 0.0f);
		glowyArea = temp.material;

		this.originalCol = glowyArea.GetColor("_TintColor");

	}
	
	// Update is called once per frame
	void Update () 
	{
		Color col = this.originalCol;
		float alpha = (Mathf.Sin (Time.time * 3.0f) * 0.4f) + 0.5f;
		col.a = alpha;
		glowyArea.SetColor ("_TintColor", col);

	}
}
