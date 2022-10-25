using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceButton : MonoBehaviour
{
	[SerializeField] private Button button;
	[SerializeField] private ResourceType resourceType;

	public void Craft()
	{
		GameManager.Instance.CraftingManager.Craft(resourceType);
	}

	public void UpdateUI()
	{
		button.interactable = GameManager.Instance.CraftingManager.CanCraft(resourceType);
	}
}