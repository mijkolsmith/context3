using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainBarrel : Resource, ICraftable
{
	private Dictionary<ResourceType, int> resourcesNeeded = new()
	{
		{ ResourceType.Wood, 2 },
		{ ResourceType.Plastic, 5 }
	};
	public override ResourceType GetResourceType() => ResourceType.RainBarrel;

	public void Craft()
	{

	}

	public Dictionary<ResourceType, int> GetResourcesNeeded() => resourcesNeeded;
}