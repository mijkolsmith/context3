using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Train : MonoBehaviour, IInteractable
{
    [SerializeField] private PlayerController player;
    private SequenceManager sequenceManager;
    private Outline objectOutline;

    private void Start()
    {
        objectOutline = GetComponent<Outline>();
        sequenceManager = GameManager.Instance.SequenceManager;
    }

    public void Highlight(Color color)
    {
        objectOutline.OutlineWidth = 5f;
        objectOutline.OutlineColor = color;
    }

    public void Interact()
    {
        int randomInt = Random.Range(0, 10000);
        Debug.Log("Time traveling to: " + randomInt);
        sequenceManager.TimeTravel(randomInt);
    }
}
