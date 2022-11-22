using NaughtyAttributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DorienPopupWindow : PopupWindow
{
	[SerializeField] private GameObject popupWindow;
	public override PopupWindowType GetPopupWindowType() => PopupWindowType.Dorien;

	//Animation
	[SerializeField] private CameraController cameraController;
	[SerializeField] private GameObject scrollviewContent;
	private float startDistanceToPlayer;
	private float startHeight;

	//UI
	/*Please keep these unused variables for now, -Michael 22/11/22
	/private Dictionary<ResourceType, int> inventory = new();
	/[SerializeField] private Dictionary<ResourceType, Image> resourceSprites = new();*/
	[SerializeField, ReadOnly] private List<InventoryUIElement> resourceUIElements = new();

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
		if (popupWindow.activeInHierarchy)
		{
			popupWindow.SetActive(false);

			cameraController.objectToFollow = PlayerController.Player.gameObject;
			cameraController.cameraDistance = startDistanceToPlayer;
			cameraController.cameraHeight = startHeight;
		}
		else
		{
			popupWindow.SetActive(true);
			if (!resourceUIElements.Any()) resourceUIElements = GetComponentsInChildren<InventoryUIElement>(true).ToList();
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
		foreach (InventoryUIElement inventoryUIElement in resourceUIElements)
		{
			inventoryUIElement.UpdateUI();
		}
	}
}