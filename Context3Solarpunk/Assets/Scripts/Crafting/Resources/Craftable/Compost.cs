using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compost : Resource, ICraftable
{
	private Dictionary<ResourceType, int> resourcesNeeded = new()
	{
		{ ResourceType.Leaf, 9 }
	};
	public override ResourceType GetResourceType() => ResourceType.Compost;
	public void Craft()
	{

	}

	public Dictionary<ResourceType, int> GetResourcesNeeded() => resourcesNeeded;
}