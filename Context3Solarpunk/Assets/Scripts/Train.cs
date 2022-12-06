using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Train : MonoBehaviour, IInteractable
{
    [SerializeField] private PlayerControllerPointClick player;

    private SequenceManager sequenceManager;
    [SerializeField] private bool isInteractable;
    //For outline
    private Outline objectOutline;
    private bool highlighting;

    public bool IsInteractable { get => isInteractable; set => isInteractable = value; }

    private void Start()
    {
        objectOutline = GetComponent<Outline>();
        sequenceManager = GameManager.Instance.SequenceManager;
    }
    private void Update()
    {
        if (!highlighting) objectOutline.OutlineWidth = 0f;
        highlighting = false;
    }

    public void Highlight(Color color)
    {
        if (isInteractable)
        {
            objectOutline.OutlineWidth = 5f;
            objectOutline.OutlineColor = color;
            highlighting = true;
        } 
        else
        {
            objectOutline.OutlineWidth = 5f;
            objectOutline.OutlineColor = Color.red;
            highlighting = true;
        }
    }

    public void SetInteractable(bool isInteractable)
    {
        IsInteractable = isInteractable;
    }

    public void Interact()
    {
        if (IsInteractable)
        {
            GameManager.Instance.SequenceManager.TimeTravel();
        }
        //Do the sequencemanager time travel thing
    }
}
