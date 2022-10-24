using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electronics : Resource, ICraftable
{
	private Dictionary<ResourceType, int> resourcesNeeded = new()
	{
		{ ResourceType.OldElectronics, 3 }
	};
	public override ResourceType GetResourceType() => ResourceType.Electronics;

	public void Craft()
	{

	}

	public Dictionary<ResourceType, int> GetResourcesNeeded() => resourcesNeeded;
}