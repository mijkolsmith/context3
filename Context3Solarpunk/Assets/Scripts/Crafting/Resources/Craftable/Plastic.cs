using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plastic : Resource, ICraftable
{
	private Dictionary<ResourceType, int> resourcesNeeded = new()
	{
		{ ResourceType.OldPlastic, 3 }
	};
	public override ResourceType GetResourceType() => ResourceType.Plastic;

	public void Craft()
	{

	}

	public Dictionary<ResourceType, int> GetResourcesNeeded() => resourcesNeeded;
}