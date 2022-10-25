using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electronics : Resource, ICraftable
{
	public override ResourceType GetResourceType() => ResourceType.Electronics;

	public void Craft()
	{

	}
}