using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMovingTrainState : State
{
	private float unitBreakTimer = 0f;
	//exact numbers need to be tested
	private float unitBreakTimeNeeded = Random.Range(5f, 10f);
	public bool isBroken = false;

	private float happinessTickdownTimer = 0f;
	private float happinessTickdownTimeNeeded = 5f;

	public override IEnumerator Start()
	{
		yield return null;
	}

	public override IEnumerator Update()
	{
		//Unit breaking mechanic
		if (!isBroken) unitBreakTimer += Time.deltaTime;
		if (unitBreakTimer > unitBreakTimeNeeded)
		{
			isBroken = true;
			GameManager.Instance.TrainManager.BreakUnit(Random.Range(0, 2));
			unitBreakTimeNeeded = Random.Range(25f, 30f);
			unitBreakTimer = 0;
		}

		//Passenger happiness mechanic
		happinessTickdownTimer += Time.deltaTime;
		if (happinessTickdownTimer > happinessTickdownTimeNeeded)
		{
			GameManager.Instance.ChangePassengerHappiness(-1);
			happinessTickdownTimer = 0;
		}
		yield return null;
	}

	public override IEnumerator Exit()
	{
		yield return null;
	}
}