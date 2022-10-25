using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uniform : Resource, ICraftable
{
	public override ResourceType GetResourceType() => ResourceType.Uniform;

	public void Craft()
	{

	}
}