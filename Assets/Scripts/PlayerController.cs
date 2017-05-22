using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public enum State {ground, moving, jumping, air,};
	public State state;
	public float speed = 3.0f;
	public float jumpPower = 3.0f;


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
		bool isJumping = moveVertical > 0;
		bool isCrouching = moveVertical < 0;
		//bool isAir = false;

		animator.SetBool ("isMoving", isMoving);
		if (isMoving) {
			animator.SetFloat ("moveX", moveHorizontal);
			transform.localScale = new Vector3 (1.0f * moveHorizontal, 1.0f, 1.0f);
		}

		if(state == State.ground) animator.SetBool("isJumping", isJumping);
		if (isJumping && state == State.ground) {
			state = State.jumping;
			//body.AddForce (Vector2.up * jumpPower, ForceMode2D.Impulse);
			Vector3 jumpVelocity = new Vector3(0.0f, 1.0f, 0.0f);
			body.velocity = jumpVelocity * jumpPower;
			//isAir = true;
		}
	}

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag == "Ground" && state == State.jumping) {
			state = State.ground;
		}
	}
	void OnCollisionExit2D(Collision2D collision){
		if (collision.gameObject.tag == "Ground" && state == State.ground) {
			state = State.air;
		}
	}
}
