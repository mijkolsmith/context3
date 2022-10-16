using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrainState : State
{
	private bool canMove;

	private float moveTimer = 0f;
	private float moveTimeNeeded = 3f;

	public override IEnumerator Start()
	{
		canMove = true;
		yield return null;
	}

	public override IEnumerator Update()
	{
		yield return null;
		if (canMove)
		{
			moveTimer += Time.deltaTime;
			if (moveTimer > moveTimeNeeded)
			{
				GameManager.Instance.EnvironmentManager.ToggleTrain();
			}
		}
	}

	public override IEnumerator Exit()
	{
		yield return null;
		canMove = false;
	}
}