using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public enum State {ground, moving, jumping, air, neutral, attack}; //TODO Need to have access to multiple states simultaneously
	public State state;
	public float speed = 3.0f;
	public float jumpForce = 3.0f;
	public GameObject basicHitbox;

	private Rigidbody2D body;
	private Animator animator;
	private SpriteRenderer sprite;

	//Inputs
	public float Horizontal{
		get { return horizontal; } 
	}
	public float Vertical{
		get { return vertical; }
	}

	float horizontal;
	float vertical;
	bool isMoving;
	bool isJumping;
	bool isCrouching;
	bool isAttacking;


	void Start(){
		body = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		sprite = GetComponent<SpriteRenderer>();
		basicHitbox.SetActive (false);
	}

	void FixedUpdate(){
		//Debug.Log (state);
		horizontal = Input.GetAxisRaw ("Horizontal");
		vertical = Input.GetAxisRaw ("Vertical");
		body.velocity = new Vector2(horizontal * speed, body.velocity.y);
		if (state == State.attack)
			body.velocity = new Vector2 (0, 0);
		//if (state == State.attack && inAir);

		isMoving = (Mathf.Abs (horizontal)) > 0;
		isJumping = vertical > 0;
		animator.SetBool ("isJumping", isJumping);
		isCrouching = vertical < 0;
		isAttacking = Input.GetKeyDown (KeyCode.Z);
		//bool isAir = false;

		animator.SetBool ("isMoving", isMoving);
		if (isMoving) {
			animator.SetFloat ("moveX", horizontal);
			transform.localScale = new Vector3 (1.0f * horizontal, 1.0f, 1.0f);
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
			//isAir = true;
		}

		if (isAttacking && state != State.attack) {
			StartCoroutine ("Attack");
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

	IEnumerator Attack(){
		state = State.attack;
		animator.SetBool("isAttacking", true);
		yield return new WaitForSeconds (0.05f);
		basicHitbox.SetActive (true);
		yield return new WaitForSeconds(0.20f);
		basicHitbox.SetActive (false);
		yield return new WaitForSeconds(0.20f);
		animator.SetBool("isAttacking", false);
		state = State.ground;
		//state = State.neutral;
	}
}
