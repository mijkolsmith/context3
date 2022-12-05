using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Train : MonoBehaviour, IInteractable
{
    [SerializeField] private PlayerControllerPointClick player;
    
    private SequenceManager sequenceManager;

    //For outline
    private Outline objectOutline;
    private bool highlighting;

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
        objectOutline.OutlineWidth = 5f;
        objectOutline.OutlineColor = color;
        highlighting = true;
    }

    public void Interact()
    {
        //Do the sequencemanager time travel thing
        int randomInt = Random.Range(0, 10000);
        sequenceManager.TimeTravel(randomInt);
    }
}
