using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompostHeap : Resource, ICraftable
{
	private Dictionary<ResourceType, int> resourcesNeeded = new()
	{
		{ ResourceType.Metal, 3 }
	};
	public override ResourceType GetResourceType() => ResourceType.CompostHeap;
	public void Craft()
	{

	}

	public Dictionary<ResourceType, int> GetResourcesNeeded() => resourcesNeeded;
}