using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Isolation : Resource, ICraftable
{
	private Dictionary<ResourceType, int> resourcesNeeded = new()
	{
		{ ResourceType.Rubber, 3 },
		{ ResourceType.Fabric, 3 }
	};
	public override ResourceType GetResourceType() => ResourceType.Isolation;
	public void Craft()
	{

	}

	public Dictionary<ResourceType, int> GetResourcesNeeded() => resourcesNeeded;
}