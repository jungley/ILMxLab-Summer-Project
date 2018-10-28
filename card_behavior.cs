using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card_behavior : StoryInteractable {

	float time = 0.0f;
	public GameObject cardBlinkyArea;

	public GameObject outlineGlow;

	//Make a card Explosion with ADD-EXPLOSION-FORCE where all the cards on the table are being affected
	protected override void Start ()
	{
		//set all BlinkyAreas to ture
		cardBlinkyArea.SetActive (true);
		//cardBlinkyArea2.SetActive (true);
		//cardBlinkArea3.SetActive (true);
	}



	//Basically detect if held onto longer than 2 seconds
	void OnTriggerStay(Collider other)
	{
		if (other.tag == "CardPlacement" && !isComplete)
		{
			isComplete = true;
			//other.gameObject.SetActive (false);
			//cardBlinkyArea.SetActive(false);
			Destroy(other.gameObject);
			outlineGlow.SetActive (false);


			EventManager.cardsCompleted++;
			if(EventManager.cardsCompleted == 3)
				CompletedInteraction ();

		}
    }


	//If card falls through table
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "tableRespawn") 
		{
			respawn (transform.parent.position.x, originalSpot.y, transform.position.z);
		}
	}
		

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "CardPlacement") 
		{
			//reset cause u a newb
			time = 0.0f;
		
		}
	}
}
