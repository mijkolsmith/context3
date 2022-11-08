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
    public bool sequential;
    public QuestState state;


    [HideInInspector] public int taskNmbr = 0;
    [HideInInspector] public Task currentTask;
    public List<Task> tasks;
    [SerializeField, Header("The event that activates when the quest is activated.")] private UnityEvent activateEvent;

    [SerializeField, Header("The event that activates when quest is complete.")] private UnityEvent succesEvent;
}