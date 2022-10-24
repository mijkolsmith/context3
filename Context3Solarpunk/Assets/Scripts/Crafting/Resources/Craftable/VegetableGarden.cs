using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetableGarden : Resource, ICraftable
{
	private Dictionary<ResourceType, int> resourcesNeeded = new()
	{
		{ ResourceType.Compost, 3 },
		{ ResourceType.RainWater, 3 }
	};
	public override ResourceType GetResourceType() => ResourceType.VegetableGarden;
	public void Craft()
	{

	}

	public Dictionary<ResourceType, int> GetResourcesNeeded() => resourcesNeeded;
}