using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestActivator : MonoBehaviour
{
    [SerializeField] private int questIdToActivate;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("QuestActivator called");
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.QuestManager.currentQuest == null)
            {
                GameManager.Instance.QuestManager.StartQuestByID(questIdToActivate);
            }
        }
    }
}
