using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
	private RectTransform rectTransform;
	private Vector3 velocity = Vector3.zero;
	[SerializeField] private float dampingSpeed = .005f;
	[SerializeField] private CraftingPopupWindow craftingPopupWindow;
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
	/// Initialize some variables and return a new instance with these variables.
	/// </summary>
	/// <param name="resourceType"></param>
	/// <param name="sprite"></param>
	/// <param name="craftingPopupWindow"></param>
	/// <returns>This initialized GameObject</returns>
	public GameObject Initialize(ResourceType resourceType, Sprite sprite, CraftingPopupWindow craftingPopupWindow)
	{
		this.resourceType = resourceType;
		image.sprite = sprite;
		this.craftingPopupWindow = craftingPopupWindow;
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
		transform.SetParent(craftingPopupWindow.transform);
	}

	/// <summary>
	/// Unity default interface function to check if a drag is ended.
	/// Fill a slot of the craftingWindow requirements.
	/// </summary>
	/// <param name="eventData"></param>
	public void OnEndDrag(PointerEventData eventData)
	{
		RectTransform craftingPanelRectTransform = craftingPopupWindow.GetCraftingPanelRectTransform();
		Vector2 localMousePosition = craftingPanelRectTransform.InverseTransformPoint(Input.mousePosition);
		if (craftingPanelRectTransform.rect.Contains(localMousePosition))
		{
			craftingPopupWindow.Fill(resourceType);
			GameManager.Instance.SoundManager.PlayOneShotSound(SoundName.ITEM_PICKUP);
		}
		Destroy(gameObject);
	}
}