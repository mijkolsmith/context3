using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RecycleBinUiElement : DraggableUiElement
{
	[field: SerializeField] protected override ResourceType ResourceType { set; get; }
	[field: SerializeField] public override Image Image { get; set; }
	public override PopupWindow PopupWindow { get; set; }
	public override ResourceType GetResourceType() => ResourceType;

	/// <summary>
	/// Unity default interface function to check if a drag is initialized.
	/// Change the eventData with a new initialized object which is draggable.
	/// </summary>
	/// <param name="eventData"></param>
	public override void OnInitializePotentialDrag(PointerEventData eventData)
	{
		if (PopupWindow != null)
		{
			eventData.pointerDrag = PopupWindow.GetComponent<RecycleBinPopupWindow>().SpawnDraggableObject(transform.position, ResourceType, Image.sprite);
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
}