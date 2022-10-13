using UnityEngine;

public class Trashcan : MonoBehaviour, IInteractable
{
	[SerializeField] private int cleanHappinessValue = 15;
	public TrashType trashType;

	public void Interact()
	{
		Recycle();
	}

	private void Recycle()
	{
		if (GameManager.Instance.RemoveTrashFromInventory(trashType))
		{
			GameManager.Instance.ChangePassengerHappiness(cleanHappinessValue);
		}
	}
}