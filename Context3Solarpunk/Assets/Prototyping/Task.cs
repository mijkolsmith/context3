using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[System.Serializable]
public class Task
{
    public string taskName;
    public TaskType type;

    [SerializeField] private GameObject objectToInteract;
    
    public bool success;
}
