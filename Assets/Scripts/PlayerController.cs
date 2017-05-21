using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 3.0f;

	private enum state {idle, moving};
	private Rigidbody2D body;
	private Animator animator;
	private SpriteRenderer sprite;

	void Start(){
		body = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		sprite = GetComponent<SpriteRenderer>();
	}



	void FixedUpdate(){
		float moveHorizontal = Input.GetAxisRaw ("Horizontal");
		float moveVertical = Input.GetAxisRaw ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		body.velocity = movement * speed;

		bool isMoving = (Mathf.Abs (moveHorizontal)) > 0;

		animator.SetBool ("isMoving", isMoving);
		if (isMoving) {
			animator.SetFloat ("moveX", moveHorizontal);
			transform.localScale = new Vector3 (1.0f * moveHorizontal, 1.0f, 1.0f);
		}



	}

}
