using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InPastOnPlatformState : State
{
	private float trashTimer = 0f;
	private float trashTimeNeeded = Random.Range(10f, 15f); // This should depend on how far you've progressed in the game

	public override IEnumerator Start()
	{
		yield return null;
	}

	public override IEnumerator Update()
	{
		//Trash game mechanic
		if (GameManager.Instance.trashCount <= 3) trashTimer += Time.deltaTime;
		if (trashTimer > trashTimeNeeded)
		{
			GameManager.Instance.trashCount++;
			// Spawn a trash somewhere on the platform
			trashTimeNeeded = Random.Range(10f, 15f);
			trashTimer = 0;
		}
		yield return null;
	}

	public override IEnumerator Exit()
	{
		GameManager.Instance.trashCount = 0;
		yield return null;
	}
}