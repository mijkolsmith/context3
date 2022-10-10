using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCheatMenu : MonoBehaviour
{
    private EventManager eventManager;

    private void Awake()
    {
        eventManager = GetComponent<EventManager>();
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 70, 50, 30), "Train event"))
            eventManager.TrainEvent.Invoke();
    }
}
