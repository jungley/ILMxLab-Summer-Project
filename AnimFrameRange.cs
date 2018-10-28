using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Frame Range Data type for splitting animation
public class AnimFrameRange : MonoBehaviour
{
	public GameObject animObj;
	private Animator Anim;
	private string AnimName;
	bool unLockUpdate = false;


	public AnimFrameRange(string aname, GameObject aObj)
	{
		animObj = aObj;
		Anim = aObj.GetComponent<Animator> ();
		AnimName = aname;
	}

	public void PlayAnimation()
	{
		animObj.SetActive (true);
		unLockUpdate = true;
		Anim.Play (AnimName);
	}
		
	void Update()
	{
		Debug.Log ("Update working now");
		if (unLockUpdate) {
			Debug.Log (Anim.GetCurrentAnimatorStateInfo (0).normalizedTime);
			Debug.Log (Anim.GetCurrentAnimatorStateInfo (0).length);

			if (Anim.GetCurrentAnimatorStateInfo (0).normalizedTime > Anim.GetCurrentAnimatorStateInfo (0).length)
			{
				animObj.SetActive (false);
				EventManager.DequeueFunc ();
				unLockUpdate = false;
			}
		}
	}
}
