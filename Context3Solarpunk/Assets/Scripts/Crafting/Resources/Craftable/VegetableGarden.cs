using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetableGarden : Resource, ICraftable
{
	public override ResourceType GetResourceType() => ResourceType.VegetableGarden;
	public void Craft()
	{

	}
}