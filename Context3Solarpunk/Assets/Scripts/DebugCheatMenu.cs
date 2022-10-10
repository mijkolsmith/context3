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
        if (GUI.Button(new Rect(30, 70, 25, 30), "<"))
            eventManager.TrainEventLeft.Invoke();

        if (GUI.Button(new Rect(55, 70, 100, 30), "Home"))
            eventManager.TrainEventHome.Invoke();

        if (GUI.Button(new Rect(155, 70, 25, 30), ">"))
            eventManager.TrainEventRight.Invoke();
    }
}
