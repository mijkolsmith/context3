using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    [Header("Resources")]
    [SerializeField] private List<ResourceType> resources = new();

    private Dictionary<ResourceType, Dictionary<ResourceType, int>> craftingRecipes = new()
    {
        { ResourceType.OldPlastic, new() { } },
        { ResourceType.Bottle, new() { } },
        { ResourceType.OldElectronics, new() { } },
        { ResourceType.Can, new() { } },
        { ResourceType.OldUniform, new() { } },
        { ResourceType.Leaf, new() { } },
        { ResourceType.Furniture, new() { } },
        { ResourceType.RainWater, new() { } },

        { ResourceType.Plastic, new() { { ResourceType.OldPlastic, 3 } } },
        { ResourceType.Glass, new() { { ResourceType.Bottle, 3 } } },
        { ResourceType.Electronics, new() { { ResourceType.OldElectronics, 3 } } },
        { ResourceType.Metal, new() { { ResourceType.Can, 3 } } },
        { ResourceType.Fabric, new() { { ResourceType.OldUniform, 3 } } },
        { ResourceType.Compost, new() { { ResourceType.Leaf, 9 } } },
        { ResourceType.Wood, new() { { ResourceType.Furniture, 5 } } },

        { ResourceType.SeparatedBin, new() { { ResourceType.Plastic, 3 } } },
        { ResourceType.SunPanel, new() { { ResourceType.Glass, 3 }, { ResourceType.Electronics, 3 } } },
        { ResourceType.LedLamp, new() { { ResourceType.Glass, 3 }, { ResourceType.Metal, 3 } } },
        { ResourceType.Uniform, new() { { ResourceType.Fabric, 3 } } },
        { ResourceType.Mug, new() { { ResourceType.Glass, 3 } } },
        { ResourceType.CompostHeap, new() { { ResourceType.Metal, 3 } } },
        { ResourceType.Grass, new() { { ResourceType.Compost, 3 } } },
        { ResourceType.InsectHotel, new() { { ResourceType.Wood, 2 } } },
        { ResourceType.RainBarrel, new() { { ResourceType.Wood, 2 }, { ResourceType.Plastic, 3 } } },
        { ResourceType.VegetableGarden, new() { { ResourceType.Compost, 3 }, { ResourceType.RainWater, 3 } } }
    };

    //PROBABLY not needed anymore but I don't wanna retype it if I need it
    /*private Dictionary<ResourceType, Type> resourceTypeMap = new()
	{
        { ResourceType.OldPlastic, typeof(OldPlastic) },
        { ResourceType.Bottle, typeof(Bottle) },
        { ResourceType.OldElectronics, typeof(OldElectronics) },
        { ResourceType.Can, typeof(Can) },
        { ResourceType.OldUniform, typeof(OldUniform) },
        { ResourceType.Leaf, typeof(Leaf) },
        { ResourceType.Furniture, typeof(Furniture) },
        { ResourceType.RainWater, typeof(RainWater) },

        { ResourceType.Plastic, typeof(Plastic) },
        { ResourceType.Glass, typeof(Glass) },
        { ResourceType.Electronics, typeof(Electronics) },
        { ResourceType.Metal, typeof(Metal) },
        { ResourceType.Fabric, typeof(Fabric) },
        { ResourceType.Compost, typeof(Compost) },
        { ResourceType.Wood, typeof(Wood) },

        { ResourceType.SeparatedBin, typeof(SeparatedBin) },
        { ResourceType.SunPanel, typeof(SunPanel) },
        { ResourceType.LedLamp, typeof(LedLamp) },
        { ResourceType.Uniform, typeof(Uniform) },
        { ResourceType.Mug, typeof(Mug) },
        { ResourceType.CompostHeap, typeof(CompostHeap) },
        { ResourceType.Grass, typeof(Grass) },
        { ResourceType.InsectHotel, typeof(InsectHotel) },
        { ResourceType.RainBarrel, typeof(RainBarrel) },
        { ResourceType.VegetableGarden, typeof(VegetableGarden) },
    };*/


    [SerializeField] private bool showDebugInfo = true;
    [Header("Debug")]
    [SerializeField, ShowIf("showDebugInfo")] private ResourceType debugResourceToAdd;

	[Button(enabledMode: EButtonEnableMode.Playmode), ShowIf("showDebugInfo")]
    public void DebugAddResource()
    {
        resources.Add(debugResourceToAdd);
    }

    public void AddResourceToInventory(ResourceType resourceType)
	{
        resources.Add(resourceType);
	}

    public bool CheckHasResourceInInventory(ResourceType resourceType)
	{
        return resources.Contains(resourceType);
	}

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
        ((CraftingPopupWindow) GameManager.Instance.PopupWindows.Where(x => x.GetPopupWindowType() == PopupWindowType.Crafting).FirstOrDefault()).UpdateUI();
    }

    //Do we have the right resources to craft this?
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

    // Serialization & Saving
    private void SaveInventory()
	{
        PlayerPrefs.SetInt("ResourcesCount", resources.Count);
        for (var i = 0; i < resources.Count; i++)
        {
            PlayerPrefs.SetInt("Resource" + i, (int) resources[i]);
        }
        PlayerPrefs.Save();
    }

    // Deserialization & Loading
    private void LoadInventory()
	{
        int resourceCount = PlayerPrefs.GetInt("ResourcesCount");
        for (var i = 0; i < resourceCount; i++)
        {
            resources.Add((ResourceType) PlayerPrefs.GetInt("Resource" + i));
        }
    }
}