using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [HideInInspector] public Quest currentQuest;
    private int questNmbr;
    public List<Quest> quests = new List<Quest>();
    bool questNotDone = true;

    private void Start()
    {
        if (quests.Count > 0)
        {
            StartQuest(quests[0]);
        }
    }

    public void StartQuest(Quest quest)
    {
        quest.state = QuestState.Active;
        GameManager.Instance.UiManager.QuestText.text = quest.Name;
        if (quest.sequential)
        {
            quest.currentTask = quest.tasks[0];
        }
        quest.activateEvent?.Invoke();
    }

    private void Update()
    {
        if (currentQuest != null)
        {
            for (int i = 0; i < currentQuest.tasks.Count; i++)
            {
                if (!currentQuest.tasks[i].success)
                {
                    questNotDone = true;
                }
                else
                {
                    questNotDone = false;
                }
            }
        }
        if (questNotDone == false)
        {
            currentQuest.succesEvent?.Invoke();
            currentQuest.state = QuestState.Completed;
        }
    }
}
