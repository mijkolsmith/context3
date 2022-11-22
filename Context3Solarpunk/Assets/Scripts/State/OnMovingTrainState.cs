using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class OnMovingTrainState : State
{
	public override IEnumerator Start()
	{
		yield return null;
	}

	public override IEnumerator Update()
	{
		yield return null;
	}

	public override IEnumerator Exit()
	{
		GameManager.Instance.trashCount = 0;
		yield return null;
	}
}