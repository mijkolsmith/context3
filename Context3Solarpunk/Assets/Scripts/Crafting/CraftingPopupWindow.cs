using UnityEngine;
using System.Collections.Generic;

public class CraftingPopupWindow : PopupWindow
{
	[SerializeField] private GameObject popupWindow;

	public override PopupWindowType GetPopupWindowType() => PopupWindowType.Crafting;

	public override void Open()
	{
		if (popupWindow.activeInHierarchy) popupWindow.SetActive(false); else popupWindow.SetActive(true);
	}
}