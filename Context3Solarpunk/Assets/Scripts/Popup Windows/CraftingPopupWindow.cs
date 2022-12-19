using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;

public class CraftingPopupWindow : PopupWindow
{
    [field: SerializeField] protected override GameObject PopupWindowObject { get; set; }
    [field: SerializeField] protected override GameObject EndDragPanel { get; set; }
    public override PopupWindowType GetPopupWindowType() => PopupWindowType.Crafting;
    public override RectTransform GetEndDragPanelRectTransform() => EndDragPanel.transform as RectTransform;

    [SerializeField, ReadOnly] private List<InventoryUiElement> inventoryUiElements = new();
    [SerializeField] private GameObject draggableObjectPrefab;
    [SerializeField, ReadOnly] private ResourceType resourceToCraft;
    [SerializeField] private List<GameObject> craftingUiElementPrefabs = new();
    [SerializeField] private List<CraftingUiElement> craftingUiElements = new();
    [SerializeField] private List<GameObject> craftedTextAnimationPrefabs = new();
    [SerializeField] private List<ResourceTextAnimation> craftingTextAnimations = new();
    [SerializeField] private Transform gridLayoutGroup;

    //TEMP FOR PLAYTEST 07 AND 08/12/22
    [SerializeField] private GameObject questObjectHolder;

    /// <summary>
    /// The Toggle method gets called from the PopupWindow Class.
    /// Clear old crafting UI Elements and animations.
    /// Creates new crafting UI Elements.
    /// Grabs the inventory UI Elements.
    /// </summary>
    public override void Toggle()
    {
        if (PopupWindowObject.activeInHierarchy)
        {
            ClearCraftingUIElements();

            PopupWindowObject.SetActive(false);
            GameManager.Instance.UiManager.popupWindowOpenType = PopupWindowType.None;
        }
        else
        {
            PopupWindowObject.SetActive(true);
            GameManager.Instance.UiManager.popupWindowOpenType = PopupWindowType.Crafting;

            //QUALITY CONTROL, will probably throw errors if the resourceToGet = ResourceType.None
            resourceToCraft = GameManager.Instance.QuestManager.GetResourceTypeFromTask();

            ClearCraftingUIElements();

            // Dynamically add the resources to fill
            Dictionary<ResourceType, int> craftingRecipe = GameManager.Instance.CraftingManager.GetCraftingRecipe(resourceToCraft);
            foreach (var resourceType in craftingRecipe.Keys.ToList())
            {
                for (int i = 0; i < craftingRecipe[resourceType]; i++)
                {
                    craftingUiElements.Add(Instantiate(craftingUiElementPrefabs.Where(x => x.GetComponent<CraftingUiElement>().GetResourceType() == resourceType).FirstOrDefault(), gridLayoutGroup).GetComponent<CraftingUiElement>());
                }
            }

            if (!inventoryUiElements.Any()) inventoryUiElements = GetComponentsInChildren<InventoryUiElement>(true).ToList();
            ClearAnimations();
            UpdateUI();
        }
    }

    /// <summary>
    /// Updates each UI element.
    /// </summary>
    public override void UpdateUI()
    {
        foreach (InventoryUiElement inventoryUIElement in inventoryUiElements)
        {
            inventoryUIElement.UpdateUI();
            if (inventoryUIElement.PopupWindow == null) inventoryUIElement.PopupWindow = this;
        }
    }

    /// <summary>
    /// Destroys all old animation objects.
    /// </summary>
    public void ClearAnimations()
    {
        craftingTextAnimations.Clear();
    }

    /// <summary>
    /// Destroys all the craftingUiElements and clears the list.
    /// </summary>
    private void ClearCraftingUIElements()
    {
        foreach (var craftingUiElement in craftingUiElements)
        {
            Destroy(craftingUiElement.gameObject);
        }
        craftingUiElements.Clear();
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

    /// <summary>
    /// Fill a spot, remove only visually from the inventory
    /// If all spots are filled, craft the resourceToCraft & clear the UI
    /// </summary>
    /// <param name="resourceType"></param>
    public void Fill(ResourceType resourceType)
    {
        if (craftingUiElements.Where(x => x.GetResourceType() == resourceType).ToList().Count > 0)
        {
            craftingUiElements.Where(x => x.GetResourceType() == resourceType && x.activated == false).FirstOrDefault().Activate();
            inventoryUiElements.Where(x => x.GetResourceType() == resourceType).FirstOrDefault().TemporarilyRemove();
            if (craftingUiElements.Where(x => x.activated == false).ToList().Count == 0)
            {//Craft the resourceToCraft

                GameManager.Instance.CraftingManager.Craft(resourceToCraft);
                StartAnimation();

                ClearCraftingUIElements();

                resourceToCraft = ResourceType.None;

                UpdateUI();

                GameManager.Instance.SoundManager.PlayOneShotSound(SoundName.CRAFTING_MACHINE);
                GameManager.Instance.QuestManager.ProgressCraftQuest();

                //TEMP FOR PLAYTEST 19/12/22
                GameManager.Instance.UiManager.TogglePopupWindow(PopupWindowType.Crafting);
                questObjectHolder.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Start a new crafting animation.
    /// </summary>
    public void StartAnimation()
    {
        craftingTextAnimations.Add(Instantiate(craftedTextAnimationPrefabs.Where(x => x.GetComponent<ResourceTextAnimation>().GetResourceType() == resourceToCraft).FirstOrDefault(), EndDragPanel.transform.position, Quaternion.identity, EndDragPanel.transform).GetComponent<ResourceTextAnimation>());
    }
}