using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mug : Resource, ICraftable
{
	private Dictionary<ResourceType, int> resourcesNeeded = new()
	{
		{ ResourceType.Glass, 3 }
	};
	public override ResourceType GetResourceType() => ResourceType.Mug;

	public void Craft()
	{

	}

	public Dictionary<ResourceType, int> GetResourcesNeeded() => resourcesNeeded;
}