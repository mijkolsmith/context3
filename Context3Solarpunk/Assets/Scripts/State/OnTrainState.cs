using System.Collections;
using UnityEngine;

public class OnTrainState : State
{
	private bool canMove;

	private float moveTimer = 0f;
	private float moveTimeNeeded = 3f;

	/// <summary>
	/// Functions as Unity Start method.
	/// </summary>
	/// <returns></returns>
	public override IEnumerator Start()
	{
		canMove = true;
		yield return null;
	}

	/// <summary>
	/// Functions as Unity Update method.
	/// If the player is on the train, start moving the environment after 3 seconds.
	/// </summary>
	/// <returns></returns>
	public override IEnumerator Update()
	{
		yield return null;
		if (canMove)
		{
			moveTimer += Time.deltaTime;
			if (moveTimer > moveTimeNeeded)
			{
				//GameManager.Instance.EnvironmentManager.ToggleTrain();
			}
		}
	}

	/// <summary>
	/// This method gets called upon starting another state.
	/// </summary>
	/// <returns></returns>
	public override IEnumerator Exit()
	{
		yield return null;
		canMove = false;
	}
}