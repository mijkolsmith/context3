using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Events;

[System.Serializable]
public class Task
{
    public string taskName;
    public TaskType type;

    public GameObject objectToInteract;
    public ResourceType resourceToGet;
    public int amountOfResourcesToGet;

    public bool success;
    [Header("The event that activates when the TASK succeeds.")] public UnityEvent succesEvent;
}
