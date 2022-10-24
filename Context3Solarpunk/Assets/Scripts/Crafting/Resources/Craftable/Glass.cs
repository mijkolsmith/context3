using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : Resource, ICraftable
{
	private Dictionary<ResourceType, int> resourcesNeeded = new()
	{
		{ ResourceType.Bottle, 3 }
	};
	public override ResourceType GetResourceType() => ResourceType.Glass;
	public void Craft()
	{

	}

	public Dictionary<ResourceType, int> GetResourcesNeeded() => resourcesNeeded;
}