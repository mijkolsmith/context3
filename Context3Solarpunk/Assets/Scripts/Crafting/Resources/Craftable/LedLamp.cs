using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedLamp : Resource, ICraftable
{
	public override ResourceType GetResourceType() => ResourceType.LedLamp;
	public void Craft()
	{

	}
}