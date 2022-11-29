using UnityEngine;

/// <summary>
/// An interface for interactable objects
/// </summary>
public interface IInteractable
{
	public void Interact();
	public void Highlight(Color color);
}