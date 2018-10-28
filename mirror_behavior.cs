using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;


public class mirror_behavior : StoryInteractable 
{
	//THESE MATERIALS ALSO IN SETUP SCRIPT
	public Material realLady;
	public Material[] realLadyMats = new Material[11];
	public Material[] copy_realLadyMats = new Material[11];
	public Material mirrorMat;

	public GameObject anim;
	public GameObject eyeball1;
	public GameObject eyeball2;

	public GameObject mirrorVFX;

	bool hasSwiveled;
	bool magicOn;
	public bool gogo = false;
	public bool Travel = false;

	public RenderTexture mirrorTex;
	public RenderTexture alternateDimTex;

	public Transform travelLoc;
	public float dashSpeed;
	public Transform mirrorScale;

	public GameObject startHead;
	public GameObject endHead;


	float amplitudeY = 0.01f;
	float omegaY = 8f;
	float index = 0;

	protected override void ActivateObject(bool offOn)
	{
		if (offOn || gogo) 
		{
			enableFlying ();

		} 
		else {
			mirrorMat.mainTexture = mirrorTex;
			magicOn = false;
		}
	}

	/*void OnTriggerExit(Collider other)
	{
		if (other.tag == "GameController") {
			GetComponentInParent<Collider> ().isTrigger = false;
		}
	} */

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "GameController" && magicOn) {
			mirrorVFX.SetActive (false);
		}
	}

	public void enableFlying()
	{
		mirrorVFX.SetActive (true);
		//Load in alternate scene
		SceneManager.LoadScene (1, LoadSceneMode.Additive);
		//Load alternate dimension viwer into mirror
		mirrorMat.mainTexture = alternateDimTex;
		//orient mirror to correct location
		mirrorScale.localScale = new Vector3 (-mirrorScale.localScale.x, mirrorScale.localScale.y, mirrorScale.localScale.z);

		DisableRB (true);
		transform.parent.rotation = Quaternion.Euler (0, 0, 0);
		transform.parent.LookAt (Vector3.zero);
		Travel = true;
		transform.parent.tag = "mirrorFlying";
		dashSpeed = 0.2f;
	}

	protected override void Start()
	{
		base.Start ();
		respawnDist = 10.0f;

		this.realLadyMats = anim.GetComponentsInChildren<SkinnedMeshRenderer> ().SelectMany (k => k.sharedMaterials).Distinct ().ToArray ();



		for (int i = 0; i < realLadyMats.Length; i++) 
		{
			/*realLadyMats [i].SetInt ("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
			realLadyMats [i].SetInt ("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);*/
			realLadyMats [i].SetInt ("_ZWrite", 0);
			/*realLadyMats [i].EnableKeyword ("_ALPHATEST_ON");
			realLadyMats [i].EnableKeyword ("_ALPHABLEND_ON");
			realLadyMats [i].DisableKeyword ("_ALPHAPREMULTIPLY_ON");
			realLadyMats [i].renderQueue = 3000;*/
			var col = realLadyMats [i].color;
			col.a = 0.0f;
			realLadyMats [i].color = col;
		}

	}
		
	protected override void Update()
	{
		//base update respawn in case object goes out of bounds
		base.Update ();
		//Detect if hitting ghost witch and fade back to reality
		Vector3 fwd = transform.TransformDirection (Vector3.back);
		RaycastHit hit;
		if (Physics.Raycast (transform.position, fwd, out hit, 20)) 
		{
			Debug.DrawRay (transform.position, fwd, Color.green);
			//COME HOLEHH SPHIRIT!
			if (hit.collider.tag == "hiddenWitch" && !isComplete && magicOn) 
			{
				//Rumble controller;
				print("Controller should be triggering");
				SteamVR_Controller.Input(1).TriggerHapticPulse(1000);
				SteamVR_Controller.Input(2).TriggerHapticPulse(1000);
				SteamVR_Controller.Input(3).TriggerHapticPulse(1000);
				//Transition 2 materials
				AlphaFadeDimensions();
			}
		}

		if (gogo) {
			ActivateObject (true);
			gogo = false;
		}
	}
		

	void FixedUpdate()
	{
		//TRAVELING FROM POINT A TO POINT B
		if (Travel) {
			Vector3 target = travelLoc.transform.position;
			if (!Mathf.Approximately ((transform.parent.position - target).sqrMagnitude, 0)) 
			{
				transform.parent.position = Vector3.MoveTowards (transform.parent.position, target, dashSpeed * Time.deltaTime);


				if(Vector3.Distance(transform.parent.position, target) > 0.2f)
				{
					//SIN WAVE TRAVEL UP AND DOWN
					index += Time.deltaTime;
					float y = amplitudeY * Mathf.Cos (omegaY * index);
					Vector3 thing = transform.parent.localPosition;
					thing.y += y;
					transform.parent.localPosition = thing; 

					//SPIN AROUND ROTATE
					transform.parent.Rotate(Vector3.up);
				}
			}

			//HAS REACHED DESTINATION - STOP TRAVELING
			else 
			{

				DisableRB (false);
				//make mirror pickup-able
				transform.parent.tag = "mirror";
				//Load alternate dimension viwer into mirror
				mirrorMat.mainTexture = alternateDimTex;
				magicOn = true;
				Travel = false;

				//animation active to true
				anim.SetActive(true);
				eyeball1.SetActive (false);
				eyeball2.SetActive (false);

				//Set original spot to table now
				originalSpot = transform.parent.position;
				originalRot = transform.parent.rotation;
			}
		} 
	}


	void AlphaFadeDimensions()
	{

		float fadeInTime = 2.0f;
		//DECREASE GHOST LADY MATERIAL
		if (copy_realLadyMats[0].color.a > 0) {

			for (int i = 0; i < copy_realLadyMats.Length; i++) 
			{
				Color Gcolor = copy_realLadyMats[i].color;
				Gcolor.a -= Time.deltaTime / fadeInTime;
				copy_realLadyMats[i].color = Gcolor;
			}
		}

		//INCREASE REAL LADY MATERIAL
		if (copy_realLadyMats[0].color.a < 0.5 && realLadyMats[0].color.a != 1) 
		{
			//Increase alpha of real lady material
			for (int i = 0; i <= realLadyMats.Length - 1; i++) 
			{
				Color Rcolor = realLadyMats[i].color;
				Rcolor.a += Time.deltaTime / fadeInTime; 
				realLadyMats[i].color = Rcolor;
			}

			//YOU HAVE SAVED HER
			if (realLadyMats[0].color.a > 1 || realLadyMats[0].color.a == 1) {
				isComplete = true;
				anim.GetComponent<Animator>().SetBool ("PlayNow", true);
				eyeball1.SetActive (true);
				eyeball2.SetActive (true);

				//CHANGE ALL MATERIALS TO OPAQUE
				ChangeAllToOpaque(true);
				CompletedInteraction ();

				//TALKING HEADS ENABLE
				startHead.SetActive(false);
				StartCoroutine (timeTillEndHead ());
			}
		}
	}

	IEnumerator timeTillEndHead()
	{
		yield return new WaitForSeconds (2.0f);
		endHead.SetActive (true);
	}

	void ChangeAllToOpaque(bool toOpaque)
	{
		int materialsNum = realLadyMats.Length;
		if (toOpaque) 
		{
			for (int i = 0; i < materialsNum; i++) {
				/*realLadyMats [i].SetInt ("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
				realLadyMats [i].SetInt ("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero); */
				realLadyMats [i].SetInt ("_ZWrite", 1);
				/*realLadyMats [i].DisableKeyword ("_ALPHATEST_ON");
				realLadyMats [i].DisableKeyword ("_ALPHABLEND_ON");
				realLadyMats [i].DisableKeyword ("_ALPHAPREMULTIPLY_ON");
				realLadyMats [i].renderQueue = -1; */
			}
		}
		//ELSE CHANGE ALL TO TRANSPARENT
	}


	void DisableRB(bool what)
	{
		transform.parent.GetComponent<Rigidbody> ().useGravity = !what;

		transform.parent.GetComponent<Collider> ().isTrigger = what;

		//reached the table, reset all rigibody stuff
		//set rotation
		if (!what) 
		{
			//turn off rigidbody force 
			transform.parent.GetComponent<Rigidbody> ().velocity = Vector3.zero;
			transform.parent.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
			//straighten it out
			transform.parent.rotation = Quaternion.Euler (0, 0, 0);
		}
	}
}
