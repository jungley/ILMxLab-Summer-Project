using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

	Animator anim;
	public Material ghostLady;
	public bool fadingIn = true;
	bool updateLock = true;
	void Start()
	{
		anim = GetComponent<Animator> ();
	}

	void Update()
	{
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("FinishedState") && updateLock)
		{
			//FADE OUT
			EventManager.DequeueFunc();
			fadingIn = false;
			updateLock = false;
		}

		AlphaFadeIn (fadingIn);
	}

	public void PlayAnimation()
	{
		anim.SetBool ("PlayNow", true);
	}

	void AlphaFadeIn(bool outin)
	{
		//Fade in
		//Increase the alpha
		if (outin) 
		{
			if (ghostLady.color.r < 1) 
			{
				Color Gcolor = ghostLady.color;
				Gcolor.r += 0.03f;
				Gcolor.g += 0.03f;
				Gcolor.b += 0.03f;
				ghostLady.color = Gcolor;
			}
		}
			
		//fade out
		else
		{
			//Decrease Alpha i.e. dissapear
			if (ghostLady.color.r > 0) 
			{
				Color Gcolor = ghostLady.color;
				Gcolor.r -= 0.03f;
				Gcolor.g -= 0.03f;
				Gcolor.b -= 0.03f;
				ghostLady.color = Gcolor;
			}
			//Alpha equals 0 or less than o
			else 
			{
				gameObject.SetActive (false);
			}
		} 
	}
}
