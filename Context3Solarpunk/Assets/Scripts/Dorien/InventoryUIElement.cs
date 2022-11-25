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
		int count = GameManager.Instance.CraftingManager.Resources.Where(x => x == resourceType).Count();
		amountText.text = count.ToString();
		gameObject.SetActive(count > 0);
	}
}