using NaughtyAttributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DorienPopupWindow : PopupWindow
{
	[field: SerializeField] protected override GameObject PopupWindowObject { get; set; }
	public override PopupWindowType GetPopupWindowType() => PopupWindowType.Dorien;

	//Animation
	[SerializeField] private CameraController cameraController;
	[SerializeField] private GameObject scrollviewContent;
	private float startDistanceToPlayer;
	private float startHeight;

	//UI
	[SerializeField, ReadOnly] private List<InventoryUiElement> inventoryUIElements = new();

	/// <summary>
	/// Grab the start distance and height in the Start method.
	/// </summary>
	private void Start()
	{
		startDistanceToPlayer = -Camera.main.transform.localPosition.z;
		startHeight = Camera.main.transform.localPosition.y;
	}

	/// <summary>
	/// The Toggle method gets called from the PopupWindow Class.
	/// It triggers the zoom-in animation to Dorien.
	/// Grabs all the UI elements.
	/// </summary>
	public override void Toggle()
	{
		if (PopupWindowObject.activeInHierarchy)
		{
			PopupWindowObject.SetActive(false);
			GameManager.Instance.UiManager.popupWindowOpenType = PopupWindowType.None;

			cameraController.objectToFollow = PlayerControllerPointClick.Player;
			cameraController.cameraDistance = startDistanceToPlayer;
			cameraController.cameraHeight = startHeight;
		}
		else
		{
			PopupWindowObject.SetActive(true);
			GameManager.Instance.UiManager.popupWindowOpenType = PopupWindowType.Dorien;

			if (!inventoryUIElements.Any()) inventoryUIElements = GetComponentsInChildren<InventoryUiElement>(true).ToList();
			UpdateUI();

			cameraController.objectToFollow = gameObject;
			cameraController.cameraDistance = 1.2f;
			cameraController.cameraHeight = 0.1f;
		}
	}

	/// <summary>
	/// Updates each UI element.
	/// </summary>
	public void UpdateUI()
	{
		foreach (InventoryUiElement inventoryUIElement in inventoryUIElements)
		{
			inventoryUIElement.UpdateUI();
		}
	}
}