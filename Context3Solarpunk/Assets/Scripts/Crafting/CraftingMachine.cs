using UnityEngine;

public class CraftingMachine : MonoBehaviour, IInteractable
{
	public void Interact()
	{
		GameManager.Instance.UiManager.TogglePopupWindow(PopupWindowType.Crafting);
	}
}