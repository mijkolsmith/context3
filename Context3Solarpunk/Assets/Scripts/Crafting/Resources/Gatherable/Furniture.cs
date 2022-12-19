public class Furniture : GatherableResource
{
	protected override ResourceType ResourceType { get; set; } = ResourceType.Furniture;
	public override ResourceType GetResourceType() => ResourceType;
}