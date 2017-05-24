using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputBuffer : MonoBehaviour {

	public Text inputBufferDisplay;

	private string inputBuffer;
	private PlayerController playerInput;
	private enum Direction{DB, D, DF, B, N, F, UB, U, UF};
	private enum Button{z, x, c};
	Direction direction;

	void Start(){
		playerInput = GetComponent<PlayerController> ();
	}
		
	void Update () {
		GetDirectionInput ();
		DirectionParse ();
		SnakeCheck ();
			
		if (inputBuffer.Length > 10) {
			inputBuffer = inputBuffer.Remove (0, 1);
		}
		SnakeCheck ();
		inputBufferDisplay.text = inputBuffer;
	}

	void CommandInputParse(string name, string directions, float time){
		
	}

	void DirectionParse(){
		switch (direction) {
			case Direction.DB:
			inputBuffer += "_db";
				break;
			case Direction.D:
			inputBuffer += "_d";
				break;
			case Direction.DF:
			inputBuffer += "_df";
				break;
			case Direction.B:
			inputBuffer += "_b";
				break;
			case Direction.N:
			//do nothing
				break;
			case Direction.F:
			inputBuffer += "_f";
				break;
			case Direction.UB:
			inputBuffer += "_ub";
				break;
			case Direction.U:
			inputBuffer += "_u";
				break;
			case Direction.UF:
			inputBuffer += "_uf";
				break;
		}
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

	void SnakeCheck(){
		if (inputBuffer.Substring (0, 0) == "_") {
			//string header = inputBuffer.Substring(0,1);
			inputBuffer.Remove (0, 0);
		}
	}
}
