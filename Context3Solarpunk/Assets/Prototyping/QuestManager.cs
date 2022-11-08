using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [HideInInspector] public Quest currentQuest;
    private int questNmbr;
    public List<Quest> quests = new List<Quest>();

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
    }
}
