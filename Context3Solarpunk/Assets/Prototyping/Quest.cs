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

    public List<Task> tasks;
    
    [SerializeField, Header("The event that activates when quest is complete.")] private UnityEvent succesEvent;
}