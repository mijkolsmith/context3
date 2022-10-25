using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : Resource, ICraftable
{
	public override ResourceType GetResourceType() => ResourceType.Wood;

	public void Craft()
	{

	}
}