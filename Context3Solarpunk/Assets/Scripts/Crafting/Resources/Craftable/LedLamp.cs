using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedLamp : Resource, ICraftable
{
	private Dictionary<ResourceType, int> resourcesNeeded = new()
	{
		{ ResourceType.Glass, 3 },
		{ ResourceType.Metal, 3 }
	};
	public override ResourceType GetResourceType() => ResourceType.LedLamp;
	public void Craft()
	{

	}

	public Dictionary<ResourceType, int> GetResourcesNeeded() => resourcesNeeded;
}