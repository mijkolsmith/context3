using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;

public class CraftingPopupWindow : PopupWindow
{
	[SerializeField] private GameObject popupWindow;

	public override PopupWindowType GetPopupWindowType() => PopupWindowType.Crafting;
	[SerializeField, ReadOnly] private List<ResourceButton> resourceButtons = new();

	public override void Open()
	{
		if (popupWindow.activeInHierarchy) popupWindow.SetActive(false);
		else
		{
			popupWindow.SetActive(true);
			if (!resourceButtons.Any()) resourceButtons = GetComponentsInChildren<ResourceButton>(true).ToList();
			foreach(ResourceButton resourceButton in resourceButtons)
			{
				resourceButton.UpdateUI();
			}
		}
	}

	public void UpdateUI()
	{
		foreach (ResourceButton resourceButton in resourceButtons)
		{
			resourceButton.UpdateUI();
		}
	}
}