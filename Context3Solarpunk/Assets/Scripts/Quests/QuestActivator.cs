using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestActivator : MonoBehaviour
{
    [SerializeField] private int questIdToActivate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //if (GameManager.Instance.QuestManager.currentQuest == null)
            //{
                Debug.Log("QuestActivator called");
                GameManager.Instance.QuestManager.StartQuestByID(questIdToActivate);
                gameObject.SetActive(false);
            //}
        }
    }
}
