using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunPanel : Resource, ICraftable
{
	private Dictionary<ResourceType, int> resourcesNeeded = new()
	{
		{ ResourceType.Glass, 3 },
		{ ResourceType.Electronics, 3 }
	};
	public override ResourceType GetResourceType() => ResourceType.SunPanel;

	public void Craft()
	{

	}

	public Dictionary<ResourceType, int> GetResourcesNeeded() => resourcesNeeded;
}