using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackQueue : MonoBehaviour {

	Queue<string> attackQueue;
	string currentAttack;
	AttackList attackList;
	InputBuffer inputBuffer;


	
	IEnumerator AttackQueueCoroutine(){
		while(enabled){
			if (attackQueue.Count > 0) {
				currentAttack = attackQueue.Dequeue();
				yield return StartCoroutine ("currentAttack");
			} 
			else {
				yield return new WaitForEndOfFrame ();	
			}
		}
	}

	void Start(){
		attackList.GetComponent<AttackList>();
		inputBuffer.GetComponent<InputBuffer>();
		
		StartCoroutine("AttackQueueCoroutine");
	}

	void Update(){
		string anInput = inputBuffer.inputB[inputBuffer.inputB.Count-1];
		if(anInput == "A"){
			attackQueue.Enqueue("normalAttackA");
		}
		if(anInput == "B"){
			attackQueue.Enqueue("normalAttackB");
		}
		if(anInput == "C"){
			attackQueue.Enqueue("normalAttackC");
		}
	}


	

}
