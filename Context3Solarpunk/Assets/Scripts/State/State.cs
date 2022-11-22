using System.Collections;

public abstract class State
{
	/// <summary>
	/// Functions as Unity Start method.
	/// </summary>
	/// <returns></returns>
	public virtual IEnumerator Start()
	{
		yield break;
	}

	/// <summary>
	/// Functions as Unity Update method.
	/// </summary>
	/// <returns></returns>
	public virtual IEnumerator Update()
	{
		yield break;
	}

	/// <summary>
	/// This method gets called upon starting another state.
	/// </summary>
	/// <returns></returns>
	public virtual IEnumerator Exit()
	{
		yield break;
	}
}