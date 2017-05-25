using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandList : MonoBehaviour {

	//Get Player Stuff
	//Get Player Inputs
	//Check if certain input is in buffer
		//If input matches, and happened recently	
			//Do something

	private PlayerController playerInput;
	private InputBuffer inputBufferComponent;
	string commandString;


	void Start(){
		playerInput = GetComponent<PlayerController> ();
		inputBufferComponent = GetComponent<InputBuffer> ();
	}

	void Update(){
		List<string> commandInput = inputBufferComponent.inputB;
		List<float> commandInputTime = inputBufferComponent.inputT;
		for(int i = 0; i < commandInput.Count; ++i)
			commandString += commandInput[i].ToString();
		Debug.Log (commandString);
		if(commandString.Contains("236a")){
			Debug.Log("HADOUKEN!!!");
		}
		commandString = "";
	}
}
