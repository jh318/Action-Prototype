using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackList : MonoBehaviour {

	public List<AnimationClip> animationList = new List<AnimationClip>();
	Animator anim;

	public class Attack
	{
		public float startUp;
		public float active;
		public float recovery;
		public float totalTime = 0;
		public BoxCollider2D hitBox;
		public AnimationClip animationClip;
		public Attack(float aStartUp, float aActive, float aRecovery, AnimationClip aAnimationClip){
			startUp = aStartUp;
			active = aActive;
			recovery = aRecovery;
			animationClip = aAnimationClip;
		}
	}

	void Start(){
		Attack attackNeutralA = new Attack (0.05f, 0.20f, 0.20f, animationList [0]);
		attackNeutralA.totalTime = attackNeutralA.startUp + attackNeutralA.active + attackNeutralA.recovery;
		anim = GetComponent<Animator> ();
	}
		
	IEnumerator NeutralA(Attack attack){
		anim.Play ("Fiora_AttackA");
		yield return new WaitForSeconds (0.05f);
		attack.hitBox.gameObject.SetActive (true);
		yield return new WaitForSeconds(0.20f);
		attack.hitBox.gameObject.SetActive (false);
		yield return new WaitForSeconds(0.20f);
	}

}
