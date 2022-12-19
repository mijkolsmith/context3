using UnityEngine;

public class RecycleBinUiElementHolder : Resource
{
    [field: SerializeField] protected override ResourceType ResourceType { get; set; }
    public override ResourceType GetResourceType() => ResourceType;
}