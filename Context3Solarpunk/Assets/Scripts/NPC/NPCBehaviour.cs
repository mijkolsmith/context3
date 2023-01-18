using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour, IInteractable
{
	[SerializeField, ReadOnly] private Outline objectOutline;
	[SerializeField] private string dialogue;
	[SerializeField] private int npcId;
	private bool highlighting = false;

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
	/// Open the crafting popup window.
	/// </summary>
	public void Interact()
	{
		GameManager.Instance.UiManager.StartNpcDialogue(dialogue, npcId);
	}

	/// <summary>
	/// Activate the highlight outline.
	/// </summary>
	/// <param name="color"></param>
	public void Highlight(Color color)
	{
		objectOutline.OutlineWidth = 5f;
		objectOutline.OutlineColor = color;
		highlighting = true;
	}
}