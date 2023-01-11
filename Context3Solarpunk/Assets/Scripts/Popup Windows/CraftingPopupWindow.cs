using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine.UI;

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
    [SerializeField] private List<ToCraftHologram> toCraftHolograms = new();
    [SerializeField, ReadOnly] private ToCraftHologram toCraftHologram;
    [SerializeField] private List<CraftedObject> craftedObjects = new();
    [SerializeField] private Transform gridLayoutGroup;
    [SerializeField] private Animator animator;

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
            ClearHolograms();
            ClearCraftingUiElements();

            PopupWindowObject.SetActive(false);
            GameManager.Instance.UiManager.popupWindowOpenType = PopupWindowType.None;
        }
        else
        {
            PopupWindowObject.SetActive(true);
            GameManager.Instance.UiManager.popupWindowOpenType = PopupWindowType.Crafting;
            ClearAnimations();

            resourceToCraft = GameManager.Instance.QuestManager.GetResourceTypeFromTask();
            if (resourceToCraft == ResourceType.None) return;

            // Activate the correct visual hologram
            toCraftHologram = toCraftHolograms.Where(x => x.GetResourceType() == resourceToCraft).FirstOrDefault();
            if (toCraftHologram != null) toCraftHologram.gameObject.SetActive(true);

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
    private void ClearCraftingUiElements()
    {
        foreach (CraftingUiElement craftingUiElement in craftingUiElements)
        {
            Destroy(craftingUiElement.gameObject);
        }
        craftingUiElements.Clear();
    }

    /// <summary>
    /// Set all the holograms to inactive gameobjects
    /// </summary>
    private void ClearHolograms()
	{
		foreach (ToCraftHologram hologram in toCraftHolograms)
		{
            hologram.gameObject.SetActive(false);
		}
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
        List<CraftingUiElement> craftingUiElementsOfResourceType = craftingUiElements.Where(x => x.GetResourceType() == resourceType).ToList();
        if (craftingUiElementsOfResourceType.Count > 0)
        {
            // Activate the first crafting UI element
            craftingUiElementsOfResourceType.Where(x => x.activated == false).FirstOrDefault().Activate();
            
            // Visually remove the first inventory UI element from the inventory
            inventoryUiElements.Where(x => x.GetResourceType() == resourceType).FirstOrDefault().TemporarilyRemove();

            // Get the correct toCraftHologram and set the progress
            toCraftHologram.SetProgress(craftingUiElements.Where(x => x.activated == true).ToList().Count / (float)craftingUiElements.Count);

            if (craftingUiElements.Where(x => x.activated == false).ToList().Count == 0)
            {
                // Set the crafted object to active
                craftedObjects.Where(x => x.GetResourceType() == resourceToCraft).FirstOrDefault().ActivateHologram();

                // Reset the crafting UI for the next craft
                ClearCraftingUiElements();
                toCraftHologram.SetProgress(0);
                toCraftHologram.gameObject.SetActive(false);
                UpdateUI();

                // Craft the resourceToCraft, start an animation, play a sound
                GameManager.Instance.UiManager.TogglePopupWindow(PopupWindowType.Crafting);
                GameManager.Instance.CraftingManager.Craft(resourceToCraft);
                GameManager.Instance.SoundManager.PlayOneShotSound(SoundName.CRAFTING_MACHINE);
                StartAnimation();

                // Reset the resourceToCraft for the next craft
                resourceToCraft = ResourceType.None;
            }
        }
    }

    /// <summary>
    /// Start a new crafting animation.
    /// </summary>
    public void StartAnimation()
    {
        craftingTextAnimations.Add(Instantiate(craftedTextAnimationPrefabs.Where(x => x.GetComponent<ResourceTextAnimation>().GetResourceType() == resourceToCraft).FirstOrDefault(), EndDragPanel.transform.position, Quaternion.identity, EndDragPanel.transform).GetComponent<ResourceTextAnimation>());
        animator.Play("Base Layer.Craft", 0, 0);
    }

    /// <summary>
    /// Set the resourcetocraft (DEPRECATED? needs testing)
    /// </summary>
    /// <param name="resourceType"></param>
    public void SetResourceToCraft(ResourceType resourceType)
	{
        resourceToCraft = resourceType;
	}
}