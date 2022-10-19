using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Quest : ScriptableObject
{
    [System.Serializable]
    public struct Info
    {
        public string Name;
        public string Description;
    }

    [Header("Info")] private Info information;

    [System.Serializable]
    public struct Stat
    {
        public int Experience;
    }

    [Header("Reward")] public Stat reward = new Stat { Experience = 1 };

    public bool Completed { get; protected set; }
    public QuestCompletedEvent QuestCompleted;

    public abstract class QuestGoal : ScriptableObject
    {
        protected string description;
        public int currentAmount { get; protected set; }
        public int requiredAmount = 1;

        public bool Completed { get; protected set; }
        [HideInInspector] public UnityEvent goalCompleted;

        public virtual string GetDescription()
        {
            return description;
        }

        public virtual void Initialize()
        {
            Completed = false;
        }
    }

    public List<QuestGoal> quests;

}

public class QuestCompletedEvent : UnityEvent<Quest>
{

}

