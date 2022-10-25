using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metal : Resource, ICraftable
{
	public override ResourceType GetResourceType() => ResourceType.Metal;
	public void Craft()
	{

	}
}