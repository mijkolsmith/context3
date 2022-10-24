using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : Resource, ICraftable
{
	private Dictionary<ResourceType, int> resourcesNeeded = new()
	{
		{ ResourceType.Compost, 3 }
	};
	public override ResourceType GetResourceType() => ResourceType.Grass;
	public void Craft()
	{

	}

	public Dictionary<ResourceType, int> GetResourcesNeeded() => resourcesNeeded;
}