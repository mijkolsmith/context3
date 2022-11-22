using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Cleanup / Remove all unnecessary code
//TODO: Add Summaries

public class QuestManager : MonoBehaviour
{
    [HideInInspector] public Quest currentQuest;
    [SerializeField] private PlayerController player;
    //private int questNmbr;
    public List<Quest> quests = new List<Quest>();
    bool questNotDone = true;

    private void Start()
    {
        //questNmbr = 0;
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
        currentQuest = quest;
        quest.activateEvent?.Invoke();
    }
    public void startsecondquest()
    {
        StartQuest(quests[1]);
    }


    public void AdvanceTasks()
    {
        bool allTasksAreDone = true;
        for (int i = 0; i < quests.Count; i++) //For all the quests
        {
            Quest q = quests[i];

            if (q.state == QuestState.Active) //Get all the active ones
            {
                if (q.sequential) //If quest is sequential
                {
                    if (q.currentTask.objectToInteract == player.InteractableGameObject)
                    {
                        q.currentTask.success = true;
                        q.currentTask.succesEvent?.Invoke();
                        q.taskNmbr++;
                        if (q.taskNmbr < q.tasks.Count)
                        {
                            q.currentTask = q.tasks[q.taskNmbr];
                        }
                    }
                }
                else //Not sequential quest
                {
                    for (int j = 0; j < q.tasks.Count; j++)
                    {
                        if (q.tasks[j].objectToInteract == player.InteractableGameObject)
                        {
                            q.tasks[j].success = true;
                            q.tasks[j].succesEvent?.Invoke();
                        }
                    }
                }
                //Check if all tasks are done
                for(int z = 0; z < q.tasks.Count; z++)
                {
                    if (!q.tasks[z].success)
                    {
                        allTasksAreDone = false;
                    }
                }
                if (allTasksAreDone)
                {
                    q.succesEvent?.Invoke();
                    q.state = QuestState.Completed;
                }
            }
        }
    }
}
