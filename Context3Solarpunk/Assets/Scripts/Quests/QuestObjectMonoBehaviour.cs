using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjectMonoBehaviour : MonoBehaviour
{
    private bool interactedWith = false;

    public bool InteractedWith { get => interactedWith; set => interactedWith = value; }
}
