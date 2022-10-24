using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : GatherableResource
{
	public override ResourceType GetResourceType() => ResourceType.Furniture;
}