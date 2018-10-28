using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potion_behavior : StoryInteractable 
{
	bool glowedOn = false;
	float duration = 10.0f;
	float timer = 0.0f;
	public GameObject attentionParticles;

	protected override void ActivateObject(bool offOn)
	{
        //JUST GLOW
		glowOn(offOn);
		glowedOn = offOn;
	}

	void OnTriggerEnter(Collider other)
	{
		if (!isComplete && other.tag == "Cauldron" && glowedOn) {
			glowOn (false);
			isComplete = true;
		}

		//turn off sparkle once player picks up potion
		if (other.tag == "GameController" || other.tag == "Cauldron") {
			if(attentionParticles != null)
				attentionParticles.SetActive (false);
		}
	}

	protected void glowOn(bool on)
	{
		//reset fade timer;
		timer = 0.0f;
		if (on) {
			//Fade into glow material
			GetComponentInChildren<Renderer> ().material = glowMat;
			if(attentionParticles != null)
				attentionParticles.SetActive (true);
			respawn (originalSpot.x, originalSpot.y, originalSpot.z);
			//StartCoroutine("FadeBetweenMaterials", true);
		} 
		else {
			//Fade into original material
			GetComponentInChildren<Renderer> ().material = origMat;
			//StartCoroutine("FadeBetweenMaterials", false);
		} 
	}

	protected override void Start()
	{
		if (GetComponentInChildren<Renderer> ().material != null)
			origMat = GetComponentInChildren<Renderer> ().material;
		glowOn (false);
		base.Start ();
	}
		

	IEnumerator FadeBetweenMaterials(bool enable)
	{
		//Duration check make sure IEnumerator closes
		if (timer < duration) {
			timer += Time.deltaTime;
			//LERPS TO GLOWY material
			if (enable) {
				GetComponentInChildren<Renderer> ().material.Lerp (origMat, glowMat, Time.deltaTime);
				print (timer);
				yield return null;
			} else {
				print ("GOT HERE IN IENUMERATOR YOYO");
				GetComponentInChildren<Renderer> ().material.Lerp (glowMat, origMat, Time.deltaTime); 
				yield return null;
			}
		} else
			timer = 0.0f;
	}
}
