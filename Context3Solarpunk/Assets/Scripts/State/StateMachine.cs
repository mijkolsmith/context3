using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
	private State state;

	protected State State { get => state; private set => state = value; }

	public void SetState(State _state)
	{
		if (State != null)
		{ StartCoroutine(State.Exit()); }
		State = _state;
		StartCoroutine(State.Start());
	}

	public State GetState()
    {
		return State;
    }

	public void Update()
	{
		StartCoroutine(State.Update());
	}
}