using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compost : Resource, ICraftable
{
	public override ResourceType GetResourceType() => ResourceType.Compost;
	public void Craft()
	{

	}
}