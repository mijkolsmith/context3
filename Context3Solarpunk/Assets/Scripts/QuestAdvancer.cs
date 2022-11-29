using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestAdvancer : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    [SerializeField] private QuestManager questManager;

    public void Interact()
    {
        questManager.AdvanceTasks();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerObject)
        {
            Interact();
        }
    }
}