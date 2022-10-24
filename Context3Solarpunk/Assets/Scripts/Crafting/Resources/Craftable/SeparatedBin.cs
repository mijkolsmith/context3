using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeparatedBin : Resource, ICraftable
{
	private Dictionary<ResourceType, int> resourcesNeeded = new()
	{
		{ ResourceType.Plastic, 3 }
	};
	public override ResourceType GetResourceType() => ResourceType.SeparatedBin;

	public void Craft()
	{

	}

	public Dictionary<ResourceType, int> GetResourcesNeeded() => resourcesNeeded;
}