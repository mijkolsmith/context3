using UnityEngine;
using NaughtyAttributes;

public abstract class StateMachine : MonoBehaviour
{
    [SerializeField, ReadOnly] private State state;
    [SerializeField, ReadOnly] private string currentStateInformation;

    protected State State { get => state; private set => state = value; }
    public string CurrentStateInformation
    {
        get => currentStateInformation;
        set => currentStateInformation = value;
    }

    public void SetState(State _state)
    {
        if (State != null)
        { StartCoroutine(State.Exit()); }
        State = _state;
        currentStateInformation = _state.ToString();
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