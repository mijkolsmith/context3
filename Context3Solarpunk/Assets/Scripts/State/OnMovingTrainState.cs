using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class OnMovingTrainState : State
{
	/// <summary>
	/// Functions as Unity Start method.
	/// </summary>
	/// <returns></returns>
	public override IEnumerator Start()
	{
		yield return null;
	}

	/// <summary>
	/// Functions as Unity Update method.
	/// </summary>
	/// <returns></returns>
	public override IEnumerator Update()
	{
		yield return null;
	}

	/// <summary>
	/// This method gets called upon starting another state.
	/// Resets the trash count.
	/// </summary>
	/// <returns></returns>
	public override IEnumerator Exit()
	{
		GameManager.Instance.trashCount = 0;
		yield return null;
	}
}