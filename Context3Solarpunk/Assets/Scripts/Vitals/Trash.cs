using UnityEngine;

public class Trash : MonoBehaviour, IInteractable
{
	[SerializeField] private int spawnHappinessValue = -1;
	[SerializeField] private int cleanHappinessValue = 15;
	public TrashType trashType;
	
	public void Interact()
	{
		Clean();
	}

	public void Spawn(TrashType trashType)
	{
		this.trashType = trashType;
		GameManager.Instance.ChangePassengerHappiness(spawnHappinessValue);
	}

	private void Clean()
	{
		GameManager.Instance.ChangePassengerHappiness(cleanHappinessValue);
		GameManager.Instance.AddTrashToInventory(trashType);
		GameManager.Instance.trashCount--;
		gameObject.SetActive(false);
	}
}