using NaughtyAttributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    [Header("Resources")]
    [SerializeField] private List<ResourceType> resources = new();

    //A dictionary which contains all of the crafting recipes.
    private Dictionary<ResourceType, Dictionary<ResourceType, int>> craftingRecipes = new()
    {
        { ResourceType.None, new() { } },

        { ResourceType.OldPlastic, new() { } },
        { ResourceType.Bottle, new() { } },
        { ResourceType.OldElectronics, new() { } },
        { ResourceType.Can, new() { } },
        { ResourceType.OldUniform, new() { } },
        { ResourceType.Leaf, new() { } },
        { ResourceType.Furniture, new() { } },
        { ResourceType.RainWater, new() { } },

        { ResourceType.SeparatedBin, new() { { ResourceType.OldPlastic, 4 } } },
        { ResourceType.SunPanel, new() { { ResourceType.Bottle, 3 }, { ResourceType.OldElectronics, 3 } } },
        { ResourceType.LedLamp, new() { { ResourceType.Bottle, 2 }, { ResourceType.Can, 4 } } },
        { ResourceType.Uniform, new() { { ResourceType.OldUniform, 5 } } },
        { ResourceType.Mug, new() { { ResourceType.Bottle, 3 } } },
        { ResourceType.CompostHeap, new() { { ResourceType.Can, 5 } } },
        { ResourceType.Grass, new() { { ResourceType.Leaf, 9 } } },
        { ResourceType.InsectHotel, new() { { ResourceType.Furniture, 3 } } },
        { ResourceType.RainBarrel, new() { { ResourceType.Furniture, 4 }, { ResourceType.OldPlastic, 3 } } },
        { ResourceType.VegetableGarden, new() { { ResourceType.Leaf, 3 }, { ResourceType.RainWater, 3 } } }
    };

    [SerializeField] private bool showDebugInfo = true;
    [Header("Debug")]
    [SerializeField, ShowIf("showDebugInfo")] private ResourceType debugResourceToAdd;

	public List<ResourceType> Resources { get => resources; private set => resources = value; }

    /// <summary>
    /// A Debug method to add resources to the inventory.
    /// </summary>
	[Button(enabledMode: EButtonEnableMode.Playmode), ShowIf("showDebugInfo")]
    public void DebugAddResource()
    {
        resources.Add(debugResourceToAdd);
    }

    /// <summary>
    /// This method gets called when picking up new resources and adds them to the inventory.
    /// </summary>
    /// <param name="resourceType"></param>
    public void AddResourceToInventory(ResourceType resourceType)
	{
        resources.Add(resourceType);
	}

    /// <summary>
    /// Check if the inventory contains a certain resource.
    /// </summary>
    /// <param name="resourceType"></param>
    /// <returns></returns>
    public bool CheckHasResourceInInventory(ResourceType resourceType)
	{
        return resources.Contains(resourceType);
	}

    /// <summary>
    /// Craft a new resource using the resources available in the inventory.
    /// Updates the crafting UI afterwards.
    /// </summary>
    /// <param name="resourceType"></param>
    public void Craft(ResourceType resourceType)
	{
        Dictionary<ResourceType, int> resourcesNeeded = craftingRecipes[resourceType];

        foreach (ResourceType resourceNeeded in resourcesNeeded.Keys)
		{
			for (int i = 0; i < resourcesNeeded[resourceNeeded]; i++)
			{
                resources.Remove(resourceNeeded);
			}
		}

        resources.Add(resourceType);
        ((CraftingPopupWindow) GameManager.Instance.UiManager.PopupWindows.Where(x => x.GetPopupWindowType() == PopupWindowType.Crafting).FirstOrDefault()).UpdateUI();
    }

    /// <summary>
    /// Checks if we have the right resources to craft an object.
    /// </summary>
    /// <param name="resourceType"></param>
    /// <param name="canCraft"></param>
    /// <returns></returns>
    public Dictionary<ResourceType, int> CanCraft(ResourceType resourceType, out bool canCraft)
    {
        Dictionary<ResourceType, int> resourcesNeeded = craftingRecipes[resourceType];
        Dictionary<ResourceType, int> resourcesCount = new();
        foreach (ResourceType resourceNeeded in resourcesNeeded.Keys)
        {
            resourcesCount.Add(resourceNeeded, 0);
        }
        List<ResourceType> tempResources = new(resources);

        foreach (ResourceType resource in resourcesNeeded.Keys)
        {
            resourcesCount[resource] = tempResources.Where(x => x == resource).Count();
        }

        foreach (ResourceType resource in resourcesNeeded.Keys)
        {
            if (resourcesCount[resource] < resourcesNeeded[resource])
            {
                canCraft = false;
                return resourcesCount;
            }
        }
        canCraft = true;
        return resourcesCount;
    }

    public Dictionary<ResourceType, int> GetCraftingRecipe(ResourceType resourceType)
	{
        return craftingRecipes[resourceType];
    }

    /// <summary>
    /// Serialization & Saving
    /// </summary>
    public void SaveInventory()
	{
        PlayerPrefs.SetInt("ResourcesCount", resources.Count);
        for (var i = 0; i < resources.Count; i++)
        {
            PlayerPrefs.SetInt("Resource" + i, (int) resources[i]);
        }
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Deserialization & Loading
    /// </summary>
    public void LoadInventory()
	{
        int resourceCount = PlayerPrefs.GetInt("ResourcesCount");
        for (var i = 0; i < resourceCount; i++)
        {
            resources.Add((ResourceType) PlayerPrefs.GetInt("Resource" + i));
        }
    }
}