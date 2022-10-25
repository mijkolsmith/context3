using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [HideInInspector] public Quest currentQuest;
    private int questNmbr;
    public List<Quest> quests = new List<Quest>();

    private void AdvanceQuest(Quest quest)
    {
        if (quest.state == QuestState.Active)
        {
            for (int i = 0; i < quest.tasks.Count; i++)
            {
                if (quest.tasks[i].objectToInteract.GetComponent<QuestObjectMonoBehaviour>().InteractedWith)
                {
                    quest.tasks[i].success = true;
                }
            }
        } else
        {
            Debug.LogWarning("Trying to advance a quest that isn't active! Quest state: " + quest.state);
        }
    }
}
