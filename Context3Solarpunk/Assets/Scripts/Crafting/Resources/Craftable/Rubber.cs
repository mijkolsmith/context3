using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rubber : Resource, ICraftable
{
	private Dictionary<ResourceType, int> resourcesNeeded = new()
	{
		{ ResourceType.Tyre, 3 }
	};
	public override ResourceType GetResourceType() => ResourceType.Rubber;

	public void Craft()
	{

	}

	public Dictionary<ResourceType, int> GetResourcesNeeded() => resourcesNeeded;
}