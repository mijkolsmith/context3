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
            if (questManager.quests[i].state == QuestState.Active) //Get all the active ones
            {
                for (int j = 0; j < questManager.quests[i].tasks.Count; j++) //Get the tasks of the active ones
                {
                    if (questManager.quests[i].tasks[j].objectToInteract == gameObject && questManager.quests[i].tasks[j].success == false) //Get this interacted object
                    {
                        questManager.quests[i].tasks[j].success = true; //Set this interacted object interactedBool success
                        questManager.quests[i].tasks[j].succesEvent?.Invoke();
                    }
                }
            }
        }
    }
}
