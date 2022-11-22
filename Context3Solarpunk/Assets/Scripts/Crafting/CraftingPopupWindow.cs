using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;

public class CraftingPopupWindow : PopupWindow
{
	[SerializeField] private GameObject popupWindow;

	public override PopupWindowType GetPopupWindowType() => PopupWindowType.Crafting;
	[SerializeField, ReadOnly] private List<ResourceButton> resourceButtons = new();

	/// <summary>
	/// The Toggle method gets called from the PopupWindow Class.
	/// Grabs all the UI elements.
	/// </summary>
	public override void Toggle()
	{
		if (popupWindow.activeInHierarchy) popupWindow.SetActive(false);
		else
		{
			popupWindow.SetActive(true);
			if (!resourceButtons.Any()) resourceButtons = GetComponentsInChildren<ResourceButton>(true).ToList();
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
}