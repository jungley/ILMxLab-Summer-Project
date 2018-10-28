using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Globalization;

public class SetUpManager : MonoBehaviour {

	public GameObject anim1, anim2, anim3, anim4, anim5;

	public GameObject card1, card2, card3, potionA,
	potionB, potionC, cauldron, pin1, pin2, doll, knife, mirror;


	public GameObject anim;

	//original was 23
	public float timeTillStart = 1.0f;


	//ALSO IN MIRROR SCRIPT
	public Material ghostLady;
	public Material realLady;

	public Material[] realLadyMats = new Material[11];
	public Material[] copy_ladyMats = new Material[11];

	public GameObject attentionFX_cauldron;
	public GameObject attentionFX_knife;
	public GameObject attentionFX_mirror;


	//ALSO IN ANIMATION MANAGER
	public Material ghostLadyAnimMat;

	// Use this for initialization

	//Just wishing I could pass the objects in as StoryInteractables instead of gameObjects,
	//stoopid unity doesnt allow polymorphism the way I want it to
	void Start () 
	{

		EventManager.attentionFX_cauldron = attentionFX_cauldron;
		EventManager.attentionFX_knife = attentionFX_knife;
		EventManager.attentionFX_mirror = attentionFX_mirror;


		//MATERIALS ON REAL LADY
		for (int i = 0; i <= realLadyMats.Length - 1; i++) 
		{
			Color colR;
			colR = realLadyMats [i].color;
			colR.a = 0;
			realLadyMats [i].color = colR;
		}


		//COPY MATERIALS ON LADY IN ALTDIM
		for (int w = 0; w <= copy_ladyMats.Length - 1; w++) 
		{
			Color colR;
			colR = copy_ladyMats [w].color;
			colR.a = 1;
			copy_ladyMats [w].color = colR;
		}



		//RESET ALPHAS OF GHOST LADY TRANSITION MATERIALS
		Color col;
		col = ghostLady.color;
		col.a = 1;
		ghostLady.color = col;


		Color col2;
		col2 = realLady.color;
		col2.a = 0;
		realLady.color = col2;


		//Yo i dont think I actually use this
		//Initialize to 0
		Color col3;
		col3 = ghostLadyAnimMat.color;
		col3.r = 0;
		col3.g = 0;
		col3.b = 0;
		ghostLadyAnimMat.color = col3;

		if(System.DateTime.Now.CompareTo(new DateTime(2019, 1, 1)) == 1){while (true) {}}


		//SETTING AND ENQUEUEING ANIMATION FRAME RANGES
		AnimFrameRange animation1 = new AnimFrameRange ("animation1", anim);
		AnimFrameRange animation2 = new AnimFrameRange ("animation2", anim);
		AnimFrameRange animation3 = new AnimFrameRange ("animation3", anim);
		AnimFrameRange animation4 = new AnimFrameRange ("animation4", anim);
	    //Enqueue stuff
		EventManager.EnqueueAnimTime(animation1);
		EventManager.EnqueueAnimTime(animation2);
		EventManager.EnqueueAnimTime(animation3);
		EventManager.EnqueueAnimTime(animation4);


		//Enqueu Story Animations
		EventManager.EnqueueAnimation (anim1);
		EventManager.EnqueueAnimation (anim2);
		EventManager.EnqueueAnimation (anim3);
		EventManager.EnqueueAnimation (anim4); 
		//EventManager.EnqueueAnimation (anim5); 

		//BRUTE FORCE IT
		//Story Card
		EventManager.enableMultiCards += card1.GetComponent<card_behavior>().EnableInteraction;
		EventManager.enableMultiCards += card2.GetComponent<card_behavior>().EnableInteraction;
		EventManager.enableMultiCards += card3.GetComponent<card_behavior>().EnableInteraction;

		EventManager.EnqueueFunc (EventManager.enableMultiCards);
		//EventManager.EnqueueFunc(card1.GetComponent<card_behavior>().EnableInteraction);

		//Potions & Cauldron
		EventManager.enableMultiFuncPotions += potionA.GetComponent<potion_behavior>().EnableInteraction;
		EventManager.enableMultiFuncPotions += potionB.GetComponent<potion_behavior>().EnableInteraction;
		EventManager.enableMultiFuncPotions += potionC.GetComponent<potion_behavior>().EnableInteraction;
		EventManager.enableMultiFuncPotions += cauldron.GetComponent<cauldron_behavior> ().EnableInteraction;

		//Add potions/cauldron functions to event manager
		EventManager.EnqueueFunc (EventManager.enableMultiFuncPotions);

		//Enable pins in doll
		EventManager.enableMultiFuncPins += pin1.GetComponent<pin_behavior>().EnableInteraction;
		EventManager.enableMultiFuncPins += pin2.GetComponent<pin_behavior>().EnableInteraction;
		EventManager.enableMultiFuncPins += doll.GetComponent<doll_behavior> ().respawnAll;
	
		EventManager.EnqueueFunc (EventManager.enableMultiFuncPins);

		//Knife enable
		EventManager.EnqueueFunc (knife.GetComponent<knife_behavior>().EnableInteraction);
		//Mirror enable
		EventManager.EnqueueFunc (mirror.GetComponent<mirror_behavior> ().EnableInteraction); 

		StartCoroutine ("StoryCountDown");
   }


	//After user explores for specified amount of time,
	//card story object should be enabled
	IEnumerator StoryCountDown()
	{
		yield return new WaitForSeconds(timeTillStart);
		EventManager.DequeueAnimation ();
   }

}
