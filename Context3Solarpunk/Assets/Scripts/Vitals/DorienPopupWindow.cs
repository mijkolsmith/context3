using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DorienPopupWindow : PopupWindow
{
	[SerializeField] private GameObject popupWindow;
	public override PopupWindowType GetPopupWindowType() => PopupWindowType.Dorien;

	[SerializeField] private CameraController cameraController;
	[SerializeField] private GameObject scrollviewContent;
	private float startDistanceToPlayer;
	private float startHeight;
	private bool open;
	private Dictionary<ResourceType, int> inventory = new();
	[SerializeField] private Dictionary<ResourceType, Image> resourceSprites = new();

	private void Start()
	{
		startDistanceToPlayer = -Camera.main.transform.localPosition.z;
		startHeight = Camera.main.transform.localPosition.y;
	}

	public override void Open()
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

			cameraController.objectToFollow = gameObject;
			cameraController.cameraDistance = 1.2f;
			cameraController.cameraHeight = 0.1f;
		}
	}

	public void LoadInventory()
	{
		foreach(var resource in GameManager.Instance.CraftingManager.Resources)
		{

		}
	}
}