using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Cleanup / Remove all unnecessary code
//TODO: Add Summaries

public class QuestManager : MonoBehaviour
{
    [HideInInspector] public Quest currentQuest;
    [SerializeField] private PlayerControllerPointClick player;
    //private int questNmbr;
    public List<Quest> quests = new List<Quest>();
    //bool questNotDone = true;

    /// <summary>
    /// Start quest 0 in Start method
    /// </summary>
    private void Start()
    {
        //questNmbr = 0;
        if (quests.Count > 0)
        {
            StartQuest(quests[0]);
        }
    }

    /// <summary>
    /// Starts the param quest by quest type
    /// </summary>
    /// <param name="quest"></param>
    public void StartQuest(Quest quest)
    {
        Debug.Log("Quest started! Quest ID: " + quest.uniqueQuestID);
        quest.state = QuestState.Active;
        GameManager.Instance.UiManager.QuestText.text = quest.Name;
        if (quest.sequential)
        {
            quest.currentTask = quest.tasks[0];
        }
        currentQuest = quest;
        quest.activateEvent?.Invoke();
    }

    /// <summary>
    /// Start the quest with the corresponding ID
    /// </summary>
    /// <param name="id"></param>
    public void StartQuestByID(int id)
    {
        for (int i = 0; i < quests.Count; i++)
        {
            if (quests[i].uniqueQuestID == id)
            {
                StartQuest(quests[i]);
            }
        }
    }


    /// <summary>
    /// Advances through the tasks of current quest and invokes all related events to succeeding a task.
    /// </summary>
    public void AdvanceTasks(IInteractable interactableObject)
    {
        bool allTasksAreDone = true;
        for (int i = 0; i < quests.Count; i++) //For all the quests
        {
            Quest q = quests[i];

            if (q.state == QuestState.Active) //Get all the active ones
            {
                if (q.sequential) //If quest is sequential
                {
                    if (q.currentTask.objectToInteract.GetComponent<IInteractable>() == interactableObject)
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
                    for (int j = 0; j < q.tasks.Count; j++) //Check for all tasks in quest
                    {
                        if (q.tasks[j].objectToInteract.GetComponent<IInteractable>() == interactableObject)
                        {
                            q.tasks[j].success = true;
                            q.tasks[j].succesEvent?.Invoke();
                        }
                    }
                }
                //Check if all tasks are done
                for (int z = 0; z < q.tasks.Count; z++)
                {
                    if (!q.tasks[z].success)
                    {
                        allTasksAreDone = false;
                    }
                }
                if (allTasksAreDone)
                {
                    Debug.Log("Quest " + q.uniqueQuestID + "is completed.");
                    q.succesEvent?.Invoke();
                    q.state = QuestState.Completed;
                    currentQuest = null;
                }
            }
        }
    }

    /// <summary>
    /// Advance the tasks that have a resourcetype to add.
    /// TODO: This needs to be rewritten
    /// </summary>
    /// <param name="resourceToCheckOn"></param>
    /*public void AdvanceGatherItemTasks(ResourceType resourceToCheckOn)
    {
        for (int i = 0; i < quests.Count; i++)
        {
            if (quests[i].state == QuestState.Active)
            {
                for (int j = 0; j < quests[i].tasks.Count; j++)
                {
                    if (quests[i].tasks[j].resourceToGet == resourceToCheckOn) //If resourcetoget has been get
                    {
                        quests[i].tasks[j].success = true;
                        quests[i].tasks[j].succesEvent?.Invoke();
                    }
                }
            }
        }
    //This call can't be made because AdvanceTasks requires an interactableObject.
    AdvanceTasks();
    }*/
}
