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

            TemporarilyDisableInteraction(.3f);

            GameManager.Instance.SoundManager.PlayOneShotSound(SoundName.TIME_TRAVEL);

            GameManager.Instance.SequenceManager.TimeTravel();

            GameManager.Instance.QuestManager.AdvanceTasks();

        }

        //Do the sequencemanager time travel thing

    }

    /// <summary>

    /// Temporarily disable interaction so you don't get spammed with sounds.

    /// </summary>

    /// <param name="seconds"></param>

    /// <returns></returns>
    public IEnumerator TemporarilyDisableInteraction(float seconds)

	{

        IsInteractable = false;

        yield return new WaitForSeconds(seconds);

        IsInteractable = true;

        yield return null;

	}
}
