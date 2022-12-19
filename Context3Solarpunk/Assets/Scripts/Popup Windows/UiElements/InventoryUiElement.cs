using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using NaughtyAttributes;

public class InventoryUiElement : DraggableUiElement
{
	[field: SerializeField] protected override ResourceType ResourceType { set; get; }
	[field: SerializeField] public override Image Image { get; set; }
	public override PopupWindow PopupWindow { get; set; }
	public override ResourceType GetResourceType() => ResourceType;

	public TextMeshProUGUI amountText;
	[SerializeField, ReadOnly] private int count;

	/// <summary>
	/// Update the inventory UI.
	/// Update it's amount and make it invisible if it's 0.
	/// </summary>
	public void UpdateUI()
	{
		count = GameManager.Instance.CraftingManager.Resources.Where(x => x == ResourceType).Count();
		amountText.text = count.ToString();
		gameObject.SetActive(count > 0);
	}

	/// <summary>
	/// Unity default interface function to check if a drag is initialized.
	/// Change the eventData with a new initialized object which is draggable.
	/// </summary>
	/// <param name="eventData"></param>
	public override void OnInitializePotentialDrag(PointerEventData eventData)
	{
		if (PopupWindow != null && count > 0)
		{
			eventData.pointerDrag = PopupWindow.GetComponent<CraftingPopupWindow>().SpawnDraggableObject(transform.position, ResourceType, Image.sprite);
		}
	}

	/// <summary>
	/// Unity default interface function to check if a drag is happening.
	/// Empty function but the dragging does not work without calling it.
	/// </summary>
	/// <param name="eventData"></param>
	public override void OnDrag(PointerEventData eventData)
	{
		//empty
	}

	/// <summary>
	/// Temporarily change the UI to display one less element.
	/// </summary>
	public void TemporarilyRemove()
	{
		count--;
		amountText.text = count.ToString();
		gameObject.SetActive(count > 0);
	}
}