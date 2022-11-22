using UnityEngine;
using NaughtyAttributes;

public abstract class StateMachine : MonoBehaviour
{
    [SerializeField, ReadOnly] private State state;
    [SerializeField, ReadOnly] private string currentStateInformation;

    /// <summary>
    /// Set a new state.
    /// Call the previous state's Exit method and the new state's Start method.
    /// </summary>
    /// <param name="_state"></param>
    public void SetState(State _state)
    {
        if (state != null)
        { StartCoroutine(state.Exit()); }
        state = _state;
        currentStateInformation = _state.ToString();
        StartCoroutine(state.Start());
    }

    /// <summary>
    /// Get the current state.
    /// </summary>
    /// <returns></returns>
    public State GetState()
    {
        return state;
    }

    /// <summary>
    /// Call the state's Update method in the Update method.
    /// </summary>
    public void Update()
    {
        StartCoroutine(state.Update());
    }
}