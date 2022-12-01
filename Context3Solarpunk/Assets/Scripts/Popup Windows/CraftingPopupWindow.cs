using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine.UI;

public class CraftingPopupWindow : PopupWindow
{
	[field: SerializeField] protected override GameObject PopupWindowObject { get; set; }

	public override PopupWindowType GetPopupWindowType() => PopupWindowType.Crafting;
	//[SerializeField, ReadOnly] private List<ResourceButton> resourceButtons = new();
	[SerializeField, ReadOnly] private List<InventoryUiElement> inventoryUiElements = new();
	[SerializeField] private GameObject draggableObjectPrefab;
	[SerializeField] private GameObject craftingPanel;
	[SerializeField, ReadOnly] private ResourceType resourceToCraft;
	[SerializeField] private List<GameObject> craftingUiElementPrefabs = new();
	[SerializeField] private List<CraftingUiElement> craftingUiElements = new();
	[SerializeField] private List<GameObject> craftedTextAnimationPrefabs = new();
	[SerializeField] private List<ResourceTextAnimation> craftingTextAnimations = new();
	[SerializeField] private Transform[] parents;

	/// <summary>
	/// The Toggle method gets called from the PopupWindow Class.
	/// Grabs all the UI elements.
	/// </summary>
	public override void Toggle()
	{
		if (PopupWindowObject.activeInHierarchy)
		{
			//Clear the Ui elements.
			foreach (var craftingUiElement in craftingUiElements)
			{
				Destroy(craftingUiElement);
			}
			craftingUiElements.Clear();

			PopupWindowObject.SetActive(false);
			GameManager.Instance.UiManager.popupWindowOpenType = PopupWindowType.None;
		}
		else
		{
			PopupWindowObject.SetActive(true);
			GameManager.Instance.UiManager.popupWindowOpenType = PopupWindowType.Crafting;

			// TODO: Get current resource to craft from the questmanager and set it
			resourceToCraft = ResourceType.SeparatedBin;
			
			// Dynamically add the resources to fill
			Dictionary<ResourceType, int> craftingRecipe = GameManager.Instance.CraftingManager.GetCraftingRecipe(resourceToCraft);
			int i = 0;
			foreach (var resourceType in craftingRecipe.Keys.ToList())
			{
				for (; i < craftingRecipe[resourceType]; i++)
				{
					Transform parent;
					if (i < 5) parent = parents[0];
					else parent = parents[1];
					craftingUiElements.Add(Instantiate(craftingUiElementPrefabs.Where(x => x.GetComponent<CraftingUiElement>().GetResourceType() == resourceType).FirstOrDefault(), parent).GetComponent<CraftingUiElement>());
				}
			}

			if (!inventoryUiElements.Any()) inventoryUiElements = GetComponentsInChildren<InventoryUiElement>(true).ToList();
			//ClearAnimations();
			UpdateUI();
		}
	}

	[Button(enabledMode: EButtonEnableMode.Playmode)]
	public void DebugUpdateUI()
	{
		GameManager.Instance.UiManager.popupWindowOpenType = PopupWindowType.Crafting;

		resourceToCraft = ResourceType.SeparatedBin;

		Dictionary<ResourceType, int> craftingRecipe = GameManager.Instance.CraftingManager.GetCraftingRecipe(resourceToCraft);
		int i = 0;
		foreach (var resourceType in craftingRecipe.Keys.ToList())
		{
			for (; i < craftingRecipe[resourceType]; i++)
			{
				Transform parent;
				if (i < 5) parent = parents[0];
				else parent = parents[1];
				craftingUiElements.Add(Instantiate(craftingUiElementPrefabs.Where(x => x.GetComponent<CraftingUiElement>().GetResourceType() == resourceType).FirstOrDefault(), parent).GetComponent<CraftingUiElement>());
			}
		}

		if (!inventoryUiElements.Any()) inventoryUiElements = GetComponentsInChildren<InventoryUiElement>(true).ToList();
		//ClearAnimations();
		UpdateUI();
	}

	/// <summary>
	/// Updates each UI element.
	/// </summary>
	public void UpdateUI()
	{
		foreach (InventoryUiElement inventoryUIElement in inventoryUiElements)
		{
			inventoryUIElement.UpdateUI();
			if (inventoryUIElement.craftingPopupWindow == null) inventoryUIElement.craftingPopupWindow = this;
		}
	}

	/// <summary>
	/// Destroys all old animation objects.
	/// </summary>
	public void ClearAnimations()
	{
		craftingTextAnimations.Clear();
	}

	public GameObject SpawnDraggableObject(Vector3 position, ResourceType resourceType, Sprite sprite)
	{
		var initializedDraggableObject = draggableObjectPrefab.GetComponent<DraggableObject>().Initialize(resourceType, sprite, this);
		return Instantiate(initializedDraggableObject, position, Quaternion.identity, transform);
	}

	public RectTransform GetCraftingPanelRectTransform()
	{
		return craftingPanel.transform as RectTransform;
	}

	/// <summary>
	/// Fill a spot, remove only visually from the inventory
	/// </summary>
	/// <param name="resourceType"></param>
	public void Fill(ResourceType resourceType)
	{
		if (craftingUiElements.Where(x => x.GetResourceType() == resourceType).ToList().Count > 0)
		{
			craftingUiElements.Where(x => x.GetResourceType() == resourceType && x.activated == false).FirstOrDefault().Activate();
			inventoryUiElements.Where(x => x.GetResourceType() == resourceType).FirstOrDefault().TemporarilyRemove();
			if (craftingUiElements.Where(x => x.activated == false).ToList().Count == 0)
			{
				GameManager.Instance.CraftingManager.Craft(resourceType);
				StartAnimation();

				//Clear the Ui elements.
				foreach (var craftingUiElement in craftingUiElements)
				{
					Destroy(craftingUiElement.gameObject);
				}
				craftingUiElements.Clear();
				resourceToCraft = ResourceType.None;

				UpdateUI();
			}
		}
	}

	public void StartAnimation()
	{
		craftingTextAnimations.Add(Instantiate(craftedTextAnimationPrefabs.Where(x => x.GetComponent<ResourceTextAnimation>().GetResourceType() == resourceToCraft).FirstOrDefault(), craftingPanel.transform.position, Quaternion.identity, craftingPanel.transform).GetComponent<ResourceTextAnimation>());
	}
}