using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : StateMachine
{ 
	private void Start()
	{
		//TEMPORARY FOR TESTING
		SetState(new OnMovingTrainState());
	}
}