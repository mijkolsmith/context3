using UnityEngine;

public abstract class Resource : MonoBehaviour, IResource
{
    public abstract ResourceType GetResourceType();
    public abstract void Gather();
}