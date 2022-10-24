using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : Resource, ICraftable
{
	private Dictionary<ResourceType, int> resourcesNeeded = new()
	{
		{ ResourceType.Furniture, 5 }
	};
	public override ResourceType GetResourceType() => ResourceType.Wood;

	public void Craft()
	{

	}

	public Dictionary<ResourceType, int> GetResourcesNeeded() => resourcesNeeded;
}