using UnityEngine.SceneManagement;

public class GameStateManager : StateMachine
{ 
	private void Start()
	{
		//For testing purposes, define the startState
		if (SceneManager.GetActiveScene().name == "TrashCollectionAndCraftingTestScene" || SceneManager.GetActiveScene().name == "PointAndClickTestScene")
		{
			SetState(new InPastOnPlatformState());
		}
		else
		{
			SetState(new InFutureOnPlatformState());
		}
	}
}