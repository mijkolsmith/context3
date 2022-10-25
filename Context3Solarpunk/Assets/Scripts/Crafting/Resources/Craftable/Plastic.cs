using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plastic : Resource, ICraftable
{
	public override ResourceType GetResourceType() => ResourceType.Plastic;

	public void Craft()
	{

	}
}