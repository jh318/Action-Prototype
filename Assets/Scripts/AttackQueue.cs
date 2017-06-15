using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackQueue : MonoBehaviour {

	Queue<string> attackQueue;
	string currentAttack;

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

}
