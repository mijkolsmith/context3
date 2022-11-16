using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class InventoryUIElement : Resource
{
	[SerializeField] private ResourceType resourceType;
	[SerializeField] public TextMeshProUGUI amountText;
	public override ResourceType GetResourceType() => resourceType;

	public void UpdateUI()
	{
		amountText.text = GameManager.Instance.CraftingManager.Resources.Where(x => x == resourceType).Count().ToString();
	}
}