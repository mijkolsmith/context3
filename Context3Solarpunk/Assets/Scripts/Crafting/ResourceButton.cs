using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;

public class ResourceButton : MonoBehaviour
{
	[SerializeField] private Button button;
	[SerializeField] private ResourceType resourceType;
	[SerializeField] private string[] resourceBaseTexts;
	[SerializeField] private TextMeshProUGUI resourceText;

	public void Craft()
	{
		GameManager.Instance.CraftingManager.Craft(resourceType);
		UpdateUI();
	}

	public void UpdateUI()
	{
		button.interactable = CanCraft();
	}

	private bool CanCraft()
	{
		Dictionary<ResourceType, int> resourcesNeeded = GameManager.Instance.CraftingManager.CanCraft(resourceType, out bool canCraft);
		int i = 0;
		foreach (ResourceType resource in resourcesNeeded.Keys)
		{
			resourceText.text = resourcesNeeded[resource] + resourceBaseTexts[i];
			i++;
		}
		return canCraft;
	}
}