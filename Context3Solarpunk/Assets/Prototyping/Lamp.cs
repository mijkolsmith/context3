using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : QuestObjectMonoBehaviour, IInteractable
{
    public void Interact()
    {
        //Er zou een betere manier moeten zijn om dit te schrijven zonder zo veel if-statements maar dit werkt voor nu.
        //Het gedrag zou eigenlijk gewoon allemaal naar de questmanager verplaatst moeten worden.
        QuestManager questManager = GameManager.Instance.QuestManager;
        for (int i = 0; i < questManager.quests.Count; i++) //For all the quests
        {
            Quest q = questManager.quests[i];

            if (q.state == QuestState.Active) //Get all the active ones
            {
                if (q.sequential)
                {
                    if (q.currentTask.objectToInteract == gameObject)
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

