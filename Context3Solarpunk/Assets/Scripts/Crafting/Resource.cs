using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Base resource class containing functionality to get the resource type and add MonoBehaviour.
/// </summary>
public abstract class Resource : MonoBehaviour
{
	protected abstract ResourceType ResourceType { get; set; }
	public abstract ResourceType GetResourceType();
}