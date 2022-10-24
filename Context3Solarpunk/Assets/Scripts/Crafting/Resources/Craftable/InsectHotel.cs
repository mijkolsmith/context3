using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectHotel : Resource, ICraftable
{
	private Dictionary<ResourceType, int> resourcesNeeded = new()
	{
		{ ResourceType.Wood, 2 }
	};
	public override ResourceType GetResourceType() => ResourceType.InsectHotel;
	public void Craft()
	{

	}

	public Dictionary<ResourceType, int> GetResourcesNeeded() => resourcesNeeded;
}