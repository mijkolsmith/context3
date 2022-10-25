using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mug : Resource, ICraftable
{
	public override ResourceType GetResourceType() => ResourceType.Mug;

	public void Craft()
	{

	}
}