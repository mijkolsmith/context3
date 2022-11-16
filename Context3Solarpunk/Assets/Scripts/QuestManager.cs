using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [HideInInspector] public Quest currentQuest;
    [SerializeField] private PlayerController player;
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

    public void AdvanceTasks()
    {
        bool allTasksAreDone = false;
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
            }
            else
            {
                for (int j = 0; j < q.tasks.Count; j++) //Get the tasks of the active ones
                {
                    if (q.tasks[j].objectToInteract == gameObject && q.tasks[j].success == false) //Get this interacted object
                    {
                        q.tasks[j].success = true; //Set this interacted object interactedBool success
                        q.tasks[j].succesEvent?.Invoke(); //Activate the event when task succeeds
                    }
                }
            }
        }
    }
}
