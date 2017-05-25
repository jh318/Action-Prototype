using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputBuffer : MonoBehaviour {

	public Text inputBufferDisplay;
	public List<string> inputB{
		get{return inputBuffer; }
	}
	public List<float> inputT{
		get { return inputTime; }
	}

	private List<string> inputBuffer = new List<string>();
	private List<float> inputTime = new List<float> ();
	private PlayerController playerInput;
	private Vector2 lastAxisInput;
	private enum Direction{DB, D, DF, B, N, F, UB, U, UF};
	private enum Button{a, b, c, None};
	Direction direction;
	Button button;



	void Start(){
		playerInput = GetComponent<PlayerController> ();
		inputBufferDisplay.text = "";
		//StartCoroutine (FlushBufferTimed(5));
	}
		
	void Update () {
		GetDirectionInput ();
		GetButtonInput ();
		DirectionParse ();
		ButtonParse ();

		FlushOldInputs (10); //Clear list head if greater than some# of elements
	}

	void CommandInputParse(string name, string directions, float time){
		
	}

	void GetDirectionInput(){
		Direction lastDir = Direction.N;
		if (lastAxisInput.sqrMagnitude > 0.25f) {
			if (Vector2.Angle (Vector2.up, lastAxisInput) < 22.5f) {
				lastDir = Direction.U;
			}
			else if (Vector2.Angle (Vector2.down, lastAxisInput) < 22.5f) {
				lastDir = Direction.D;
			}
			else if (Vector2.Angle (Vector2.left, lastAxisInput) < 22.5f) {
				lastDir = Direction.B;
			}
			else if (Vector2.Angle (Vector2.right, lastAxisInput) < 22.5f) {
				lastDir = Direction.F;
			}
			else if (Vector2.Angle (Vector2.one, lastAxisInput) < 22.5f) {
				lastDir = Direction.UF;
			}
			else if (Vector2.Angle (new Vector2(1,-1), lastAxisInput) < 22.5f) {
				lastDir = Direction.DF;
			}
			else if (Vector2.Angle (-Vector2.one, lastAxisInput) < 22.5f) {
				lastDir = Direction.DB;
			}
			else if (Vector2.Angle (new Vector2(-1,1), lastAxisInput) < 22.5f) {
				lastDir = Direction.UB;
			} 
		}

		Direction dir = Direction.N;
		Vector2 axisInput = new Vector2 (playerInput.Horizontal, playerInput.Vertical);
		if (axisInput.sqrMagnitude > 0.25f) {
			if (Vector2.Angle (Vector2.up, axisInput) < 22.5f) {
				dir = Direction.U;
			}
			else if (Vector2.Angle (Vector2.down, axisInput) < 22.5f) {
				dir = Direction.D;
			}
			else if (Vector2.Angle (Vector2.left, axisInput) < 22.5f) {
				dir = Direction.B;
			}
			else if (Vector2.Angle (Vector2.right, axisInput) < 22.5f) {
				dir = Direction.F;
			}
			else if (Vector2.Angle (Vector2.one, axisInput) < 22.5f) {
				dir = Direction.UF;
			}
			else if (Vector2.Angle (new Vector2(1,-1), axisInput) < 22.5f) {
				dir = Direction.DF;
			}
			else if (Vector2.Angle (-Vector2.one, axisInput) < 22.5f) {
				dir = Direction.DB;
			}
			else if (Vector2.Angle (new Vector2(-1,1), axisInput) < 22.5f) {
				dir = Direction.UB;
			} 
		}

		if (dir != Direction.N && dir != lastDir) {
			direction = dir;
		} else {
			direction = Direction.N;
		}

		lastAxisInput = new Vector2 (playerInput.Horizontal, playerInput.Vertical);
	}

	void GetButtonInput(){
		button = Button.None;
		if (playerInput.a)
			button = Button.a;
		if (playerInput.b)
			button = Button.b;
		if (playerInput.c)
			button = Button.c;
	}
		
	void ButtonParse(){
		switch (button) {
			case Button.a:
				inputBuffer.Add ("a");
				inputTime.Add (Time.time);
				break;
			case Button.b:
				inputBuffer.Add ("b");
				inputTime.Add (Time.time);
				break;
			case Button.c:
				inputBuffer.Add ("c");
				inputTime.Add (Time.time);
				break;
			case Button.None:
				//Do nothing
				break;
			default:
				//do nothing for now
				break;
		}

		if(button != Button.None)
			DisplayLastInput ();
	}

	void DirectionParse(){
		switch (direction) {
			case Direction.DB:
			inputBuffer.Add ("1");
			inputTime.Add (Time.time);
				break;
			case Direction.D:
			inputBuffer.Add ("2");
			inputTime.Add (Time.time);
				break;
			case Direction.DF:
			inputBuffer.Add ("3");
			inputTime.Add (Time.time);
				break;
			case Direction.B:
			inputBuffer.Add("4");
			inputTime.Add (Time.time);
				break;
			case Direction.N:
			//do nothing
				break;
			case Direction.F:
			inputBuffer.Add ("6");
			inputTime.Add (Time.time);
				break;
			case Direction.UB:
			inputBuffer.Add ("7");
			inputTime.Add (Time.time);
				break;
			case Direction.U:
			inputBuffer.Add ("8");
			inputTime.Add (Time.time);
				break;
			case Direction.UF:
			inputBuffer.Add("9");
			inputTime.Add (Time.time);
				break;
		}

		if (direction != Direction.N) 
			DisplayLastInput ();	
	}

	void DisplayLastInput(){
		if (Input.anyKeyDown && inputBuffer.Count > 0) {
			inputBufferDisplay.text += inputBuffer [inputBuffer.Count-1];
		}
		if (inputBufferDisplay.text.Length > 25) {
			inputBufferDisplay.text = inputBufferDisplay.text.Remove (0, 10);
		}
	}

	IEnumerator DisplayInputBufferCoroutine(){
		while (enabled) {
			for (int i = 0; i < inputBuffer.Count; ++i) {
				inputBufferDisplay.text += inputBuffer[i];
			}
			yield return new WaitForSeconds (1.0f);
		}
	}

	public void FlushBuffer(){
		inputBuffer.Clear();
	}

	void FlushOldInputs(int inputNumber){
		if (inputBuffer.Count > inputNumber)
			inputBuffer.RemoveAt(0);
	}

	IEnumerator FlushBufferTimed(float time){
		while (enabled) {
			yield return new WaitForSeconds (time);
			inputBuffer.Clear ();
		}
	}
		
}
