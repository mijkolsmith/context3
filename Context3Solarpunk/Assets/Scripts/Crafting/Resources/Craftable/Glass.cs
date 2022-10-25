using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : Resource, ICraftable
{
	public override ResourceType GetResourceType() => ResourceType.Glass;
	public void Craft()
	{

	}
}