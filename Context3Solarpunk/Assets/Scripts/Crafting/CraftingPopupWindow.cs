using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;

public class CraftingPopupWindow : PopupWindow
{
	[field: SerializeField] protected override GameObject PopupWindowObject { get; set; }

	public override PopupWindowType GetPopupWindowType() => PopupWindowType.Crafting;
	[SerializeField, ReadOnly] private List<ResourceButton> resourceButtons = new();

	/// <summary>
	/// The Toggle method gets called from the PopupWindow Class.
	/// Grabs all the UI elements.
	/// </summary>
	public override void Toggle()
	{
		if (PopupWindowObject.activeInHierarchy)
		{
			PopupWindowObject.SetActive(false);
			GameManager.Instance.popupWindowOpenType = PopupWindowType.None;
			
		}
		else
		{
			PopupWindowObject.SetActive(true);
			GameManager.Instance.popupWindowOpenType = PopupWindowType.Crafting;
			if (!resourceButtons.Any()) resourceButtons = GetComponentsInChildren<ResourceButton>(true).ToList();
			ClearAnimations();
			UpdateUI();
		}
	}

	/// <summary>
	/// Updates each UI element.
	/// </summary>
	public void UpdateUI()
	{
		foreach (ResourceButton resourceButton in resourceButtons)
		{
			resourceButton.UpdateUI();
		}
	}

	/// <summary>
	/// Destroys all old animation objects.
	/// </summary>
	public void ClearAnimations()
	{
		foreach (ResourceButton resourceButton in resourceButtons)
		{
			resourceButton.ClearCraftedTextObjects();
		}
	}
}