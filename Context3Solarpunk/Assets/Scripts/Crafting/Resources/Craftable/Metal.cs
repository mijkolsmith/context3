using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metal : Resource, ICraftable
{
	private Dictionary<ResourceType, int> resourcesNeeded = new()
	{
		{ ResourceType.Can, 3 }
	};
	public override ResourceType GetResourceType() => ResourceType.Metal;
	public void Craft()
	{

	}

	public Dictionary<ResourceType, int> GetResourcesNeeded() => resourcesNeeded;
}