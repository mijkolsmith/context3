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

	/// <summary>
	/// Update the inventory UI.
	/// Update it's amount and make it invisible if it's 0.
	/// </summary>
	public void UpdateUI()
	{
		//TODO: make invisible if 0
		amountText.text = GameManager.Instance.CraftingManager.Resources.Where(x => x == resourceType).Count().ToString();
	}
}