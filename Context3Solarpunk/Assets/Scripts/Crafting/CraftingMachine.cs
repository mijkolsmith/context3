using NaughtyAttributes;
using UnityEngine;

public class CraftingMachine : MonoBehaviour, IInteractable
{
	[SerializeField, ReadOnly] private Outline objectOutline;
	private bool highlighting;

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
		if (!highlighting) objectOutline.OutlineWidth = 0f;
		highlighting = false;
	}

	public void Highlight(Color color)
	{
		objectOutline.OutlineWidth = 5f;
		objectOutline.OutlineColor = color;
		highlighting = true;
	}
}