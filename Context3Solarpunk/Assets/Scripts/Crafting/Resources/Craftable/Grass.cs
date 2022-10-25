using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : Resource, ICraftable
{
	public override ResourceType GetResourceType() => ResourceType.Grass;
	public void Craft()
	{

	}
}