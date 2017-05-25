using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputBuffer : MonoBehaviour {

	public Text inputBufferDisplay;

	private List<string> inputBuffer = new List<string>();
	private List<float> inputTime = new List<float> ();
	private PlayerController playerInput;
	private enum Direction{DB, D, DF, B, N, F, UB, U, UF};
	private enum Button{z, x, c};
	Direction direction;


	void Start(){
		playerInput = GetComponent<PlayerController> ();
		inputBufferDisplay.text = "";
	}
		
	void Update () {
		GetDirectionInput ();
		DirectionParse ();
		DisplayLastInput ();

	}

	void CommandInputParse(string name, string directions, float time){
		
	}

	void GetDirectionInput(){
		if (playerInput.Horizontal == 0 && playerInput.Vertical == 0) //5
			direction = Direction.N;
		if (playerInput.Horizontal < 0 && playerInput.Vertical < 0) //1
			direction = Direction.DB;
		if (playerInput.Horizontal == 0 && playerInput.Vertical < 0) //2
			direction = Direction.D;
		if (playerInput.Horizontal > 0 && playerInput.Vertical < 0) //3
			direction = Direction.DF;
		if (playerInput.Horizontal < 0 && playerInput.Vertical == 0) //4
			direction = Direction.B;
		//if(playerInput.Horizontal == 0 && playerInput.Vertical == 0) //5
		//direction = Direction.N;
		if (playerInput.Horizontal > 0 && playerInput.Vertical == 0) //6
			direction = Direction.F;
		if (playerInput.Horizontal < 0 && playerInput.Vertical > 0) //7
			direction = Direction.UB;
		if (playerInput.Horizontal == 0 && playerInput.Vertical > 0) //8
			direction = Direction.U;
		if (playerInput.Horizontal > 0 && playerInput.Vertical > 0)//9
			direction = Direction.UF;
	}

	void DirectionParse(){
		switch (direction) {
			case Direction.DB:
			inputBuffer.Add ("_db"); 
				break;
			case Direction.D:
			inputBuffer.Add ("_d");
				break;
			case Direction.DF:
			inputBuffer.Add ("_df");
				break;
			case Direction.B:
			inputBuffer.Add("_b");
				break;
			case Direction.N:
			//do nothing
				break;
			case Direction.F:
			inputBuffer.Add ("_f");
				break;
			case Direction.UB:
			inputBuffer.Add ("_ub");
				break;
			case Direction.U:
			inputBuffer.Add ("_u");
				break;
			case Direction.UF:
			inputBuffer.Add("_uf");
				break;
		}
	}

	void DisplayLastInput(){
		if (Input.anyKeyDown && inputBuffer.Count > 0) {
			inputBufferDisplay.text += inputBuffer [inputBuffer.Count-1];
		}
		if (inputBufferDisplay.text.Length > 25) {
			inputBufferDisplay.text = inputBufferDisplay.text.Remove (0, 10);
		}
	}

	void DisplayCurrentInputBufferAndRemoveHead(){
		if (inputBuffer.Count > 10) {
			inputBuffer.RemoveAt (0);
		}

		for (int i = 0; i < inputBuffer.Count; ++i) {
			inputBufferDisplay.text += inputBuffer[i];
		}
	}

}
