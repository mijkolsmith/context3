using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;

public class RecycleBinPopupWindow : PopupWindow
{
    [field: SerializeField] protected override GameObject PopupWindowObject { get; set; }
    [field: SerializeField] protected override GameObject EndDragPanel { get; set; }
    public override PopupWindowType GetPopupWindowType() => PopupWindowType.RecycleBin;
    public override RectTransform GetEndDragPanelRectTransform() => EndDragPanel.transform as RectTransform;

    [SerializeField, ReadOnly] private List<InventoryUiElement> inventoryUiElements = new();
    [SerializeField] private List<GameObject> recycleBinUiElementPrefabs = new();
    [SerializeField] private List<Transform> recycleBinUiElementHolders = new();
    [SerializeField] private List<RecycleBinUiElement> recycleBinUiElements = new();

    [SerializeField] private GameObject draggableObjectPrefab;


    /// <summary>
    /// The Toggle method gets called from the PopupWindow Class.
    /// Grabs the inventory UI Elements.
    /// </summary>
    public override void Toggle()
    {
        if (PopupWindowObject.activeInHierarchy)
        {
            ClearRecycleBinUiElements();

            PopupWindowObject.SetActive(false);
            GameManager.Instance.UiManager.popupWindowOpenType = PopupWindowType.None;
        }
        else
        {
            PopupWindowObject.SetActive(true);
            GameManager.Instance.UiManager.popupWindowOpenType = PopupWindowType.RecycleBin;

            // Dynamically add recycleBinUiElements
            // TODO: Make this a dynamic system
            Dictionary<ResourceType, int> recycleBinContents = new()
            {
                { ResourceType.Bottle, 3 },
                { ResourceType.Can, 3 },
                { ResourceType.OldPlastic, 0 },
                { ResourceType.Leaf, 0 },
            };

            foreach (var resourceType in recycleBinContents.Keys.ToList())
            {
                for (int i = 0; i < recycleBinContents[resourceType]; i++)
                {
                    // Add each resourceType UiElement to the respective UiElementHolder and to a list
                    recycleBinUiElements.Add(Instantiate(recycleBinUiElementPrefabs.Where(x => x.GetComponent<RecycleBinUiElement>().GetResourceType() == resourceType).FirstOrDefault(), 
                        recycleBinUiElementHolders.Where(x => x.GetComponent<RecycleBinUiElementHolder>().GetResourceType() == resourceType).FirstOrDefault()).GetComponent<RecycleBinUiElement>());
                }
            }

            if (!inventoryUiElements.Any()) inventoryUiElements = GetComponentsInChildren<InventoryUiElement>(true).ToList();
            UpdateUI();
        }
    }

    /// <summary>
    /// Updates each UI element.
    /// </summary>
    public override void UpdateUI()
    {
        foreach (InventoryUiElement inventoryUiElement in inventoryUiElements)
        {
            inventoryUiElement.UpdateUI();
        }

        foreach (RecycleBinUiElement recycleBinUiElement in recycleBinUiElements)
        {
            if (recycleBinUiElement.PopupWindow == null) recycleBinUiElement.PopupWindow = this;
        }
    }

    private void ClearRecycleBinUiElements()
    {
        foreach (var recycleBinUiElement in recycleBinUiElements)
        {
            Destroy(recycleBinUiElement.gameObject);
        }
        recycleBinUiElements.Clear();
    }

    /// <summary>
    /// Create a new Draggable Object on given position with given resourceType and Sprite.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="resourceType"></param>
    /// <param name="sprite"></param>
    /// <returns>Draggable Object as GameObject</returns>
    public GameObject SpawnDraggableObject(Vector3 position, ResourceType resourceType, Sprite sprite)
    {
        var initializedDraggableObject = draggableObjectPrefab.GetComponent<DraggableObject>().Initialize(resourceType, sprite, this);
        return Instantiate(initializedDraggableObject, position, Quaternion.identity, transform);
    }

    public void AddResourceToInventory(ResourceType resourceType)
	{
        RecycleBinUiElement recycleBinUiElement = recycleBinUiElements.Where(x => x.GetResourceType() == resourceType).FirstOrDefault();
        recycleBinUiElements.Remove(recycleBinUiElement);
        Destroy(recycleBinUiElement.gameObject);

        GameManager.Instance.SoundManager.PlayOneShotSound(SoundName.MENU_SELECT);

        GameManager.Instance.CraftingManager.AddResourceToInventory(resourceType);
        UpdateUI();
    }
}