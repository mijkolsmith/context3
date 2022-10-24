using UnityEngine;
using System.Collections.Generic;

public class CraftingPopupWindow : PopupWindow
{
	[SerializeField] private GameObject popupWindow;

	[Header("Resources")]
	[SerializeField] private List<Resource> rescources;

	[Header("Craftables")]
	[SerializeField] private List<Craftable> craftables;

	public override PopupWindowType GetPopupWindowType() => PopupWindowType.Crafting;

	public override void Open()
	{
		if (popupWindow.activeInHierarchy) popupWindow.SetActive(false); else popupWindow.SetActive(true);
		UpdateResources();
	}

	private void UpdateResources()
	{

	}
}