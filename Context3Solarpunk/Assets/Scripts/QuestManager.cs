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
        if (quest.sequential)
        {
            quest.currentTask = quest.tasks[0];
            GameManager.Instance.UiManager.QuestText.text = quest.currentTask.taskName;
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
        for (int i = 0; i < quests.Count; i++) //For all the quests
        {
            Quest q = quests[i];

            if (q.state == QuestState.Active) //Get all the active ones
            {
                //If quest is sequential
                if (q.sequential)
                {
                    ProgressSequentialQuest(q);
                }
                else //Not sequential
                {
                    for (int j = 0; j < q.tasks.Count; j++)
                    {
                        switch (q.tasks[j].type)
                        {
                            case TaskType.None:
                                break;
                            case TaskType.Interact:
                                ProgressInteractQuest(q.tasks[j], interactableObject);
                                break;
                            case TaskType.Craft:
                                //ProgressCraftQuest(q);
                                break;
                            default:
                                break;
                        }
                    }
                }

                //Check if all tasks are done
                CheckIfAllTasksAreDone(q);
            }
        }
    }

    /// <summary>
    /// Progress the task in sequential quests 
    /// </summary>
    /// <param name="q"></param>
    public void ProgressSequentialQuest(Quest q)
    {
        q.currentTask.success = true;
        q.currentTask.succesEvent?.Invoke();
        GameManager.Instance.UiManager.QuestText.text = q.currentTask.taskName;

        q.taskNmbr++;
        if (q.taskNmbr < q.tasks.Count)
        {
            q.currentTask = q.tasks[q.taskNmbr];
        }
    }

    /// <summary>
    /// Progress a quest where you have to interact with an assigned interactable
    /// </summary>
    /// <param name="task"></param>
    /// <param name="interactable"></param>
    public void ProgressInteractQuest(Task task, IInteractable interactable)
    {

        //if (task.objectToInteract == null) continue;
        if (task.objectToInteract.GetComponent<IInteractable>() == interactable)
        {
            task.success = true;
            task.succesEvent?.Invoke();
        }

    }

    /// <summary>
    /// Get resource type from task, used by crafting machine to know what has to be crafted
    /// </summary>
    /// <returns></returns>
    public ResourceType GetResourceTypeFromTask()
    {
        if (currentQuest != null)
        {
            for (int j = 0; j < currentQuest.tasks.Count; j++) //Check for all tasks in quest
            {
                if (!currentQuest.tasks[j].success && currentQuest.tasks[j].resourceToGet != ResourceType.None)
                {
                    Debug.Log("Got resourcetoget!");
                    return currentQuest.tasks[j].resourceToGet;
                }
            }
        }
        Debug.Log("Didn't get resourcetoget!");
        return ResourceType.None;
    }

    /// <summary>
    /// Progress a quest where you have to craft something.
    /// </summary>
    /// <param name="q"></param>
    public void ProgressCraftQuest(Quest q)
    {
        for (int j = 0; j < q.tasks.Count; j++) //Check for all tasks in quest
        {
            if (q.tasks[j].resourceToGet != ResourceType.None)
            {
                q.tasks[j].success = true;
                q.tasks[j].succesEvent?.Invoke();
            }
        }
    }

    /// <summary>
    /// Progress a quest where you have to craft something using currentquest instead of a specific one by parameter
    /// </summary>
    public void ProgressCraftQuest()
    {
        for (int j = 0; j < currentQuest.tasks.Count; j++) //Check for all tasks in quest
        {
            if (currentQuest.tasks[j].resourceToGet != ResourceType.None)
            {
                currentQuest.tasks[j].success = true;
                currentQuest.tasks[j].succesEvent?.Invoke();
            }
        }
        CheckIfAllTasksAreDone(currentQuest);
    }

    private bool CheckIfAllTasksAreDone(Quest q)
    {
        bool allTasksAreDone = true;
        for (int j = 0; j < q.tasks.Count; j++)
        {
            if (!q.tasks[j].success)
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
        return allTasksAreDone;
    }
}