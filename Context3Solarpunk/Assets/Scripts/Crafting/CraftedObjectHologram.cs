using NaughtyAttributes;
using UnityEngine;

public class CraftedObjectHologram : MonoBehaviour, IInteractable
{
	[SerializeField, ReadOnly] private Outline objectOutline;
	private bool highlighting = false;
	[SerializeField] private CraftedObject craftedObject;

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
	/// Activate the craftedObject by placing it
	/// </summary>
	public void Interact()
	{
		craftedObject.Activate();
	}

	/// <summary>
	/// Activate the highlight outline.
	/// </summary>
	/// <param name="color"></param>
	public void Highlight(Color color)
	{
		objectOutline.OutlineWidth = 5f;
		objectOutline.OutlineColor = Color.blue;
		highlighting = true;
	}
}