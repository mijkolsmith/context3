using UnityEngine;

/// <summary>
/// Base resource class containing functionality to get the resource type and monobehaviour.
/// </summary>
public abstract class Resource : MonoBehaviour
{
	public abstract ResourceType GetResourceType();
}