using UnityEngine;

/// <summary>
/// Base resource class containing base resource functionality to get the resource type.
/// </summary>
public abstract class Resource : MonoBehaviour
{
	public abstract ResourceType GetResourceType();
}