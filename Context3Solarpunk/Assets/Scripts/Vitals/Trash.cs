using UnityEngine;

public class Trash : MonoBehaviour, IInteractable
{
	[SerializeField] private int spawnHappinessValue = -1;
	[SerializeField] private int cleanHappinessValue = 15;
	
	public void Interact()
	{
		Clean();
	}

	public void Spawn()
	{
		GameManager.Instance.ChangePassengerHappiness(spawnHappinessValue);
	}

	private void Clean()
	{
		GameManager.Instance.ChangePassengerHappiness(cleanHappinessValue);
		gameObject.SetActive(false);
	}
}