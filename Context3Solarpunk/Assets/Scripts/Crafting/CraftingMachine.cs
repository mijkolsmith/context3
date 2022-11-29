using NaughtyAttributes;
using UnityEngine;

public class CraftingMachine : MonoBehaviour, IInteractable
{
	[SerializeField, ReadOnly] private Outline objectOutline;

	private void Start()
	{
		objectOutline = GetComponent<Outline>();
	}

	public void Interact()
	{
		GameManager.Instance.UiManager.TogglePopupWindow(PopupWindowType.Crafting);
	}

	private void Update()
	{
		objectOutline.OutlineWidth = 0f;
	}

	public void Highlight(Color color)
	{
		objectOutline.OutlineWidth = 5f;
		objectOutline.OutlineColor = color;
	}
}