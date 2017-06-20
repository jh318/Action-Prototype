using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackQueue : MonoBehaviour {

	Queue<string> attackQueue = new Queue<string>();
	string currentAttack;
	AttackList attackList;
	InputBuffer inputBuffer;


	
	IEnumerator AttackQueueCoroutine(){
		while(enabled){
			if (attackQueue.Count > 0) {
				currentAttack = attackQueue.Dequeue();
				yield return attackList.StartCoroutine (currentAttack);
			} 
			else {
				yield return new WaitForEndOfFrame ();	
			}
		}
	}

	void Start(){
		attackList = GetComponent<AttackList>();
		inputBuffer = GetComponent<InputBuffer>();
		
		StartCoroutine("AttackQueueCoroutine");
	}

	void Update(){
		if (inputBuffer.inputB.Count == 0) return;
		string anInput = inputBuffer.inputB[inputBuffer.inputB.Count-1];
		if(anInput == "a"){
			attackQueue.Enqueue("NormalAttackA");
		}
		if(anInput == "b"){
			attackQueue.Enqueue("NormalAttackB");
		}
		if(anInput == "c"){
			attackQueue.Enqueue("NormalAttackC");
		}
	}


	

}
