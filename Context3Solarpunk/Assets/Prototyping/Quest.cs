using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Quest
{
    public string Name;
    public string Description;
    public string uniqueQuestID;
    public QuestState state;

    [SerializeField] private UnityEvent succesEvent;
    
    public List<Task> tasksToSucceed;
}