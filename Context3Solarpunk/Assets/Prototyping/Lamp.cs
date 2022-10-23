using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Player interacted with this object");
    }
}
