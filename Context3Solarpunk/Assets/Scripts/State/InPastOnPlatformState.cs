using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InPastOnPlatformState : State
{
	private float resourceSpawnTimer = 0f;
	private float resourceSpawnTimeNeeded = Random.Range(2f, 5f); // This should depend on how far you've progressed in the game / needs tweaking

	public override IEnumerator Start()
	{
		yield return null;
	}

	public override IEnumerator Update()
	{
		//Trash game mechanic
		if (GameManager.Instance.trashCount < 3) resourceSpawnTimer += Time.deltaTime;
		if (resourceSpawnTimer > resourceSpawnTimeNeeded)
		{
			GameManager.Instance.trashCount++;
			GameManager.Instance.EnvironmentManager.SpawnGatherableResource();
			resourceSpawnTimeNeeded = Random.Range(5f, 15f);
			resourceSpawnTimer = 0;
		}
		yield return null;
	}

	public override IEnumerator Exit()
	{
		GameManager.Instance.trashCount = 0;
		yield return null;
	}
}