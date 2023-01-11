using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
        else GameManager.Instance.UiManager.QuestText.text = quest.tasks[0].taskName;
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
                if (q.sequential && (q.currentTask.objectToInteract.GetComponent<IInteractable>() == interactableObject || q.currentTask.objectToInteract == null))
                {
                    ProgressSequentialQuest(q);
                }
                else //Not sequential
                {
                    foreach (Task task in q.tasks)
                    {
                        switch (task.type)
                        {
                            case TaskType.None:
                                break;
                            case TaskType.Interact:
                                ProgressInteractTask(task, interactableObject);
                                break;
                            case TaskType.Gather:
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

        q.taskNmbr++;
        if (q.taskNmbr < q.tasks.Count)
        {
            q.currentTask = q.tasks[q.taskNmbr];
        }
    }

    /// <summary>
    /// Progress a build task
    /// </summary>
    /// <param name="obj"></param>
    public void AdvanceBuildTask(CraftedObject obj)
    {
        if (currentQuest != null)
        {
            foreach (Task task in currentQuest.tasks)
            {
                if (task.type == TaskType.Build
                    && task.objectToBuild == obj
                    && !task.success)
                {
                    task.success = true;
                    task.succesEvent?.Invoke();
                    break;
                }
            }

            CheckIfAllTasksAreDone(currentQuest);
        }
    }


    /// <summary>
    /// Progress a quest where you have to interact with an assigned interactable
    /// </summary>
    /// <param name="task"></param>
    /// <param name="interactable"></param>
    public void ProgressInteractTask(Task task, IInteractable interactableObject)
    {
        if (task.type == TaskType.Interact 
            && (task.objectToInteract.GetComponent<IInteractable>() == interactableObject 
                || task.objectToInteract == null))
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
                if (!currentQuest.tasks[j].success 
                    && currentQuest.tasks[j].resourceToGet != ResourceType.None)
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
    /// Progress a gather task
    /// </summary>
    /// <param name="resourceType"></param>
    public void ProgressGatherTask(ResourceType resourceType)
    {
        if (currentQuest != null)
        {
            foreach (Task task in currentQuest.tasks)
            {
                if (task.type == TaskType.Gather
                    && task.resourceToGet == resourceType
                    && !task.success)
                {
                    task.success = true;
                    task.succesEvent?.Invoke();
                    break;
                }
            }

            CheckIfAllTasksAreDone(currentQuest);
        }
    }

    /// <summary>
    /// This returns true if all the tasks in quest Q are complete
    /// </summary>
    /// <param name="q"></param>
    /// <returns></returns>
    private bool CheckIfAllTasksAreDone(Quest q)
    {
        bool allTasksAreDone = true;
        foreach (Task task in q.tasks)
        {
            if (task.type == TaskType.None) task.success = true;
            if (!task.success)
            {
                GameManager.Instance.UiManager.QuestText.text = task.taskName;
                allTasksAreDone = false;
            }
        }

        if (allTasksAreDone)
        {
            Debug.Log("Quest " + q.uniqueQuestID + "is completed.");

            // Disable any open popup windows when quests are completed
            if (GameManager.Instance.UiManager.popupWindowOpenType != PopupWindowType.None)
			{
                GameManager.Instance.UiManager.TogglePopupWindow(GameManager.Instance.UiManager.popupWindowOpenType);
			}

            q.succesEvent?.Invoke();
            q.state = QuestState.Completed;
            currentQuest = null;
        }
        return allTasksAreDone;
    }
}