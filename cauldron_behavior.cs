using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cauldron_behavior : StoryInteractable {

	Dictionary<string, GameObject> potionStoryDict = new Dictionary<string, GameObject>();
	GameObject temp = null;
	public bool enableStory;


	public GameObject cauldronVFX;
	public GameObject cauldronLight;
	public GameObject cauldronPoof;

	public AudioSource cauldronSound;

	void OnTriggerEnter(Collider other)
	{
		//If object is potion & the cauldron has not previously interacted with it
		if (!isComplete && enableStory) {
			if ((other.tag == "potion_A" || other.tag == "potion_B" || other.tag == "potion_C") && !potionStoryDict.TryGetValue (other.tag, out temp)) {
				potionStoryDict.Add (other.tag, other.gameObject);
				EventManager.potionLevel++;


				if (EventManager.potionLevel == 3) {
					CompletedInteraction ();
					isComplete = true;
				}
			}
		}
	}


    protected override void ActivateObject(bool offOn)
	{
		enableStory = offOn;
		//TURN ON CAULDRON VFX
		if (offOn) {
			cauldronVFX.SetActive (true);
			cauldronLight.SetActive (true);
			cauldronPoof.SetActive (true);

			//TURN ON CAULDRON AUDIO
			cauldronSound.Play();
		}
	}
}


