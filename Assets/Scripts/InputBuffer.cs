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
		if (playerInput.Horizontal < 0 && playerInput.Vertical < 0) //1
			direction = Direction.DB;
		if (playerInput.Horizontal == 0 && playerInput.Vertical < 0) //2
			direction = Direction.D;
		if (playerInput.Horizontal > 0 && playerInput.Vertical < 0) //3
			direction = Direction.DF;
		if (playerInput.Horizontal < 0 && playerInput.Vertical == 0) //4
			direction = Direction.B;
		if(playerInput.Horizontal == 0 && playerInput.Vertical == 0) //5
			direction = Direction.N;
		if (playerInput.Horizontal > 0 && playerInput.Vertical == 0) //6
			direction = Direction.F;
		if (playerInput.Horizontal < 0 && playerInput.Vertical > 0) //7
			direction = Direction.UB;
		if (playerInput.Horizontal == 0 && playerInput.Vertical > 0) //8
			direction = Direction.U;
		if (playerInput.Horizontal > 0 && playerInput.Vertical > 0)//9
			direction = Direction.UF;
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

	void FlushBuffer(){
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
