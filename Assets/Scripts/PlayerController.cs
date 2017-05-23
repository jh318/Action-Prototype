using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public enum State {ground, moving, jumping, air,};
	public State state;
	public float speed = 3.0f;
	public float jumpForce = 3.0f;


	private Rigidbody2D body;
	private Animator animator;
	private SpriteRenderer sprite;

	void Start(){
		body = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		sprite = GetComponent<SpriteRenderer>();
		//StartCoroutine ("CheckState");
	}



	void FixedUpdate(){
		Debug.Log (state);

		float moveHorizontal = Input.GetAxisRaw ("Horizontal");
		float moveVertical = Input.GetAxisRaw ("Vertical");
		body.velocity = new Vector2(moveHorizontal * speed, body.velocity.y);

		bool isMoving = (Mathf.Abs (moveHorizontal)) > 0;
		bool isJumping = moveVertical > 0;
		animator.SetBool ("isJumping", isJumping);
		bool isCrouching = moveVertical < 0;
		//bool isAir = false;

		animator.SetBool ("isMoving", isMoving);
		if (isMoving) {
			animator.SetFloat ("moveX", moveHorizontal);
			transform.localScale = new Vector3 (1.0f * moveHorizontal, 1.0f, 1.0f);
		}
			
		//TODO: This is a mess vvv
		if (isMoving && isCrouching) {
			animator.SetBool ("isCrouching", false);
		} else if (isCrouching && state == State.ground && !isMoving) {
			animator.SetBool ("isCrouching", true);
		} else {
			animator.SetBool ("isCrouching", isCrouching);
		}


		if (isJumping && state == State.ground) {
			state = State.air; //Jumping
			animator.SetBool ("isJumping", true);
			animator.SetBool ("isAir", true);
			body.AddForce (Vector2.up * jumpForce);

			//Vector3 jumpVelocity = new Vector3(0.0f, 1.0f, 0.0f)*Time.deltaTime;
			//body.velocity = jumpVelocity * jumpForce * Time.deltaTime;
			//isAir = true;
		}
	}

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag == "Ground") {
			state = State.ground;
			animator.SetBool ("isGround", true);
			animator.SetBool ("isAir", false);
			animator.SetBool ("isJumping", false);
		}
	}
	void OnCollisionExit2D(Collision2D collision){
		if (collision.gameObject.tag == "Ground") {
			state = State.air;
			animator.SetBool ("isAir", true);
			animator.SetBool ("isGround", false);
			animator.SetBool ("isJumping", false);
		}
	}

	IEnumerator CheckState(){
		while (enabled) {
			Debug.Log (state);
			yield return new WaitForSeconds (2);
		}
	}
}
