using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMovingTrainState : State
{
	private float unitBreakTimer = 0f;
	//exact numbers need to be tested
	private float unitBreakTimeNeeded = Random.Range(5,10);
	public bool isBroken = false;

	public override IEnumerator Start()
	{
		yield return null;
	}

	public override IEnumerator Update()
	{
		if (!isBroken) unitBreakTimer += Time.deltaTime;
		if (unitBreakTimer > unitBreakTimeNeeded)
		{
			isBroken = true;
		}
		yield return null;
	}

	public override IEnumerator Exit()
	{
		yield return null;
	}
}