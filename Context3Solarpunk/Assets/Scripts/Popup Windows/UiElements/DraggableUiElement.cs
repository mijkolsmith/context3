using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class DraggableUiElement : Resource, IInitializePotentialDragHandler, IDragHandler
{
	protected override abstract ResourceType ResourceType { get; set; }
	public override abstract ResourceType GetResourceType();

	public abstract Image Image { get; set; }
	public abstract PopupWindow PopupWindow { get; set; }
	public abstract void OnDrag(PointerEventData eventData);
	public abstract void OnInitializePotentialDrag(PointerEventData eventData);
}