using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackList : MonoBehaviour {

	//public List<AnimationClip> animationList = new List<AnimationClip>();
	Animator anim;

	public class Attack
	{
		public float startUp;
		public float active;
		public float recovery;
		public float totalTime = 0;
		public string animName;
		public BoxCollider2D hitBox;
		public AnimationClip animationClip;
		public Attack(float startUp, float active, float recovery, string animName){
			this.startUp = startUp;
			this.active = active;
			this.recovery = recovery;
			this.animName = animName;
			this.totalTime = startUp + active + recovery;
			//this.animationClip = animationClip;
		}
	}

	void Start(){
		anim = GetComponent<Animator> ();
	}

	public IEnumerator NormalAttack(Attack attack) {
		anim.Play (attack.animName);
		yield return new WaitForSeconds (attack.startUp);
		// attack.hitBox.gameObject.SetActive (true);
		yield return new WaitForSeconds(attack.active);
		// attack.hitBox.gameObject.SetActive (false);
		yield return new WaitForSeconds(attack.recovery);
	}

	public IEnumerator NormalAttackA () {
		Attack attackNeutralA = new Attack (0.05f, 0.20f, 0.20f, "Fiora_AttackA");
		yield return StartCoroutine("NormalAttack", attackNeutralA);
	}

	public IEnumerator NormalAttackB () {
		Attack attackNeutralB = new Attack (0.08f, 0.25f, 0.3f, "Fiora_AttackB");
		yield return StartCoroutine("NormalAttack", attackNeutralB);
	}

	public IEnumerator NormalAttackC () {
		Attack attackNeutralC = new Attack (0.1f, 0.3f, 0.4f, "Fiora_AttackC");
		yield return StartCoroutine("NormalAttack", attackNeutralC);
	}

}
