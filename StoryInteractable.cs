using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Parent class for all interactable objects that advance the story
public class StoryInteractable : Interactable {

	//RigidbodyConstraints objRigid;
	public Material origMat, glowMat;
	//public GameObject poofAppear;
	protected bool isComplete = false;
	public GameObject firefly;

	public GameObject completionSound;


	//MIRROR - POTIONS - CAULDRON 
	//Override this function
	protected virtual void ActivateObject(bool offOn)
	{
		//DEACTIVATE AND DISSAPEAR // Activate AND APPEAR
		this.transform.parent.gameObject.SetActive(offOn);
	}
		
	protected virtual void Start()
	{
		ActivateObject (false);
	}

	public virtual void EnableInteraction()
	{
		ActivateObject(true);
	}    
		

	//completed state
	protected void CompletedInteraction()
	{
		//Play the Animation
		EventManager.DequeueAnimation ();
		//complete bling bling
		if(firefly != null)
			firefly.SetActive (true);

		//Play Completion Sound
		completionSound = GameObject.Find("CompletionSoundObject");
		completionSound.transform.position = transform.position;
		completionSound.GetComponent<AudioSource> ().PlayOneShot (completionSound.GetComponent<AudioSource> ().clip);


		//chandelier shake
		EventManager.shakeChandelier = true;
	}

}
