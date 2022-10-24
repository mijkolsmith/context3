using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    [Header("Resources")]
    [SerializeField] private List<Resource> resources = new();

    private Dictionary<ResourceType, Type> resourceTypeMap = new()
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
    };

    public void AddResource(Resource resource)
	{
        resources.Add(resource);
	}

    public void Craft(ResourceType craftableType)
	{
        ICraftable resource = (ICraftable) Activator.CreateInstance(resourceTypeMap[craftableType]);

        //Do we have the right resources to craft this?
        var resourcesNeeded = resource.GetResourcesNeeded();

        foreach (ResourceType requiredResource in resourcesNeeded.Keys)
		{

		}
        //resources = craftable.Craft(resources);
    }

    // Serialization & Saving
    private void SaveInventory()
	{
        PlayerPrefs.SetInt("ResourcesCount", resources.Count);
        for (var i = 0; i < resources.Count; i++)
        {
            PlayerPrefs.SetInt("Resource" + i, (int) resources[i].GetResourceType());
        }
        PlayerPrefs.Save();
    }

    // Deserialization & Loading
    private void LoadInventory()
	{
        int resourceCount = PlayerPrefs.GetInt("ResourcesCount");
        for (var i = 0; i < resourceCount; i++)
        {
            resources.Add((Resource) Activator.CreateInstance(resourceTypeMap[(ResourceType) PlayerPrefs.GetInt("Resource" + i)]));
        }
    }
}