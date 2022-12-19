using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Train : MonoBehaviour, IInteractable
{
    [SerializeField] private bool isInteractable;
    //For outline
    private Outline objectOutline;
    private bool highlighting = false;
    public bool IsInteractable { get => isInteractable; private set => isInteractable = value; }

    /// <summary>
	/// Assign Outline component in the start method.
	/// </summary>
    private void Start()
    {
        objectOutline = GetComponent<Outline>();
    }

    /// <summary>
	/// Reset the outline if it's not reactivated each frame.
	/// </summary>
    private void Update()
    {
        if (!highlighting)
        {
            objectOutline.OutlineWidth = 0f;
        }
        highlighting = false;
    }

    /// <summary>
	/// Activate the highlight outline.
	/// </summary>
	/// <param name="color"></param>
    public void Highlight(Color color)
    {
        objectOutline.OutlineWidth = 5f;
        objectOutline.OutlineColor = isInteractable ? color : Color.red;
        highlighting = true;
    }

    /// <summary>
    /// Method that can be used by a button to set the isInteractable variable.
    /// </summary>
    /// <param name="isInteractable"></param>
    public void SetInteractable(bool isInteractable)
    {
        IsInteractable = isInteractable;
    }

    /// <summary>
    /// Time travel, play sound
    /// </summary>
    public void Interact()
    {
        if (IsInteractable)
        {
			StartCoroutine(TemporarilyDisableInteraction(.3f));
			GameManager.Instance.SoundManager.PlayOneShotSound(SoundName.TIME_TRAVEL);
            GameManager.Instance.SequenceManager.TimeTravel();
            GameManager.Instance.QuestManager.AdvanceTasks(this);
        }
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