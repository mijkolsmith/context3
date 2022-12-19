using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
	private RectTransform rectTransform;
	private Vector3 velocity = Vector3.zero;
	[SerializeField] private float dampingSpeed = .005f;
	[SerializeField] private PopupWindow popupWindow;
	[SerializeField] private ResourceType resourceType;
	[SerializeField] private Image image;

	public ResourceType GetResourceType() => resourceType;

	/// <summary>
	/// Assign the rectTransform in the Awake method.
	/// </summary>
	private void Awake()
	{
		rectTransform = transform as RectTransform;
	}

	/// <summary>
	/// Because of a unity bug with onEndDrag a fix is needed in the update method.
	/// </summary>
	private void Update()
	{
		if (!Input.GetMouseButton(0))
		{
			OnEndDrag(new PointerEventData(FindObjectOfType<EventSystem>()));
		}
	}

	/// <summary>
	/// Initialize some variables and return a new instance with these variables.
	/// </summary>
	/// <param name="resourceType"></param>
	/// <param name="sprite"></param>
	/// <param name="craftingPopupWindow"></param>
	/// <returns>This initialized GameObject</returns>
	public GameObject Initialize(ResourceType resourceType, Sprite sprite, PopupWindow popupWindow)
	{
		this.resourceType = resourceType;
		image.sprite = sprite;
		this.popupWindow = popupWindow;
		return gameObject;
	}

	/// <summary>
	/// Unity default interface function to check if a drag is happening.
	/// On a drag event, change the position of the rectTransform.
	/// </summary>
	/// <param name="eventData"></param>
	public void OnDrag(PointerEventData eventData)
	{
		if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, null, out var mousePosition))
		{
			rectTransform.position = Vector3.SmoothDamp(transform.position, mousePosition, ref velocity, dampingSpeed);
		}
	}

	/// <summary>
	/// Unity default interface function to check if a drag is begun.
	/// Set the parent of this object to craftingPopupWindow so we can drag between panels.
	/// </summary>
	/// <param name="eventData"></param>
	public void OnBeginDrag(PointerEventData eventData)
	{
		transform.SetParent(popupWindow.transform);
	}

	/// <summary>
	/// Unity default interface function to check if a drag is ended.
	/// Fill a slot of the craftingWindow requirements.
	/// </summary>
	/// <param name="eventData"></param>
	public void OnEndDrag(PointerEventData eventData)
	{
		RectTransform endDragPanelRectTransform = popupWindow.GetEndDragPanelRectTransform();

		Vector2 localMousePosition = endDragPanelRectTransform.InverseTransformPoint(Input.mousePosition);
		if (endDragPanelRectTransform.rect.Contains(localMousePosition))
		{
			if (popupWindow.GetPopupWindowType() == PopupWindowType.Crafting)
			{
				((CraftingPopupWindow)popupWindow).Fill(resourceType);
			}
			else if (popupWindow.GetPopupWindowType() == PopupWindowType.RecycleBin)
			{
				((RecycleBinPopupWindow)popupWindow).AddResourceToInventory(resourceType);
			}
		}
		Destroy(gameObject);
	}
}