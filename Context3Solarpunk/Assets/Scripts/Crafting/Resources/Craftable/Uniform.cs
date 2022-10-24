using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uniform : Resource, ICraftable
{
	private Dictionary<ResourceType, int> resourcesNeeded = new()
	{
		{ ResourceType.OldUniform, 3 }
	};
	public override ResourceType GetResourceType() => ResourceType.Uniform;

	public void Craft()
	{

	}

	public Dictionary<ResourceType, int> GetResourcesNeeded() => resourcesNeeded;
}