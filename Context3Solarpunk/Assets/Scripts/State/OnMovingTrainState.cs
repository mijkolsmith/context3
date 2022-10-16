using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class OnMovingTrainState : State
{
	private float unitBreakTimer = 0f;
	//exact numbers need to be tested
	private float unitBreakTimeNeeded = Random.Range(5f, 10f);
	public bool isBroken = false;

	private float happinessTickdownTimer = 0f;
	private float happinessTickdownTimeNeeded = 5f;

	private float trashTimer = 0f;
	private float trashTimeNeeded = Random.Range(15f, 30f);

	public override IEnumerator Start()
	{
		yield return null;
	}

	public override IEnumerator Update()
	{
		//Passenger happiness game mechanic
		happinessTickdownTimer += Time.deltaTime;
		if (happinessTickdownTimer > happinessTickdownTimeNeeded)
		{
			GameManager.Instance.ChangePassengerHappiness(- GameManager.Instance.trashCount - 1);
			happinessTickdownTimer = 0;
		}
		yield return null;

		//Unit breaking game mechanic
		unitBreakTimer += Time.deltaTime;
		if (unitBreakTimer > unitBreakTimeNeeded)
		{
			//INFO: Units can break twice over, and if they break twice the indicator will disappear and be toggled the wrong way. Could be fixed but not a big problem.
			int unit = Random.Range(0, 2);
			GameManager.Instance.TrainManager.BreakUnit(unit);
			GameManager.Instance.ToggleIndicator(unit);
			unitBreakTimeNeeded = Random.Range(25f, 30f);
			unitBreakTimer = 0;
		}

		//Trash game mechanic
		if (GameManager.Instance.trashCount <= 3) trashTimer += Time.deltaTime;
		if (trashTimer > trashTimeNeeded)
		{
			GameManager.Instance.trashCount++;
			GameManager.Instance.TrainManager.train.wagons[Random.Range(0, GameManager.Instance.TrainManager.train.wagons.Count)].wagon.SpawnTrash();
			trashTimeNeeded = Random.Range(15f, 30f);
			trashTimer = 0;
			Debug.Log("spawntrash");
		}

		if (GameManager.Instance.GetPassengerHappiness() >= 100)
		{
			GameManager.Instance.EnvironmentManager.ToggleTrain();
			GameManager.Instance.ChangePassengerHappiness(-50);
		}
	}

	public override IEnumerator Exit()
	{
		yield return null;
	}
}