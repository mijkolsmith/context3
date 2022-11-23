using UnityEngine;

public class CraftingMachine : MonoBehaviour, IInteractable
{
	public void Interact()
	{
		GameManager.Instance.TogglePopupWindow(PopupWindowType.Crafting);
	}
}