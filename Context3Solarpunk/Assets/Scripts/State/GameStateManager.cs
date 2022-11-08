using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : StateMachine
{ 
	private void Start()
	{
		//For testing purposes, define the startState
		if (SceneManager.GetActiveScene().name == "TrashCollectionAndCraftingTestScene")
		{
			SetState(new InPastOnPlatformState());
		}
		else
		{
			SetState(new InFutureOnPlatformState());
		}
	}
}