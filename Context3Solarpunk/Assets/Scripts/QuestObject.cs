using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Was voorheen Lamp.cs
//TODO: Integreer GatherableResource met questprogression
public class QuestObject : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        GameManager.Instance.QuestManager.AdvanceTasks();
    }
}