using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager {

	//Delegate & Queue variables
	public delegate void EnableFunc();
	public static EnableFunc enableMultiFuncPotions;
	public static EnableFunc enableMultiFuncPins;
	public static EnableFunc enableMultiCards;

	public static Queue enableQueue = new Queue();
	public static Queue<AnimFrameRange> animTimeQueue = new Queue<AnimFrameRange>();
	public static Queue<GameObject> animationQueue = new Queue<GameObject>();



	public static bool shakeChandelier = false;

	public static int cardsCompleted = 0;


	//completion thing
	public static int currentEnableFunc = 0;
	public static GameObject attentionFX_cauldron;
	public static GameObject attentionFX_knife;
	public static GameObject attentionFX_mirror;

	//Event variables
	public static int pins_in_eye_count = 0;
	public static int potionLevel = 0;

	//Insert function into the queue
	public static void EnqueueFunc(EnableFunc s)
	{
		enableQueue.Enqueue (s);
	}
		
	//Call next function in the queue
	public static void DequeueFunc()
	{
		//if there is still something in queue
		if (enableQueue.Count != 0) {
			{
				((EnableFunc)(enableQueue.Dequeue ())) ();
				currentEnableFunc++;
			}
		}
		else
			Debug.Log ("YO DIS IS EMPTY Func");
	}

	public static void EnqueueAnimation(GameObject a)
	{
		animationQueue.Enqueue (a);
	}

	public static void DequeueAnimation()
	{
		if (animationQueue.Count != 0) {
			//Play Animation 
			//Debug.Log("DEQUEUE ANIMATION");
			(animationQueue.Dequeue ()).SetActive (true);

		} else {
			//Last Animation was knife portal animation 
			//Dequeue the func that has the mirror floating
			DequeueFunc ();
			Debug.Log ("YO DIS IS EMPTY Anim");
		}
	} 


	//Insert function into the queue
	public static void EnqueueAnimTime(AnimFrameRange f)
	{
		animTimeQueue.Enqueue (f);
	}

	//Call next function in the queue
	public static void DequeueAnimTime()
	{
		//if there is still something in queue
		if (enableQueue.Count != 0) {
			animTimeQueue.Dequeue ().PlayAnimation();
			
		}
		else
			Debug.Log ("YO DIS IS EMPTY AnimTime");
	}

	static void disableAllFX()
	{
		attentionFX_cauldron.SetActive (false);
		attentionFX_knife.SetActive (false);
		attentionFX_mirror.SetActive (false);
	}

	static IEnumerator waitTimeTillAttentionFX(int fxQueueNum)
	{
		
		yield return new WaitForSeconds (15.0f);
		switch (fxQueueNum) 
		{

		}
	}

}
