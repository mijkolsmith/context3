using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestAdvancer : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject playerObject;
    [SerializeField] private QuestManager questManager;

    public void Highlight(Color color)
    {
        //throw new System.NotImplementedException();
    }

    public void Interact()
    {
        questManager.AdvanceTasks(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerObject)
        {
            Interact();
        }
    }
}