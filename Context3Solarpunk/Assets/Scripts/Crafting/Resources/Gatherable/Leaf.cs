public class Leaf : GatherableResource
{
	protected override ResourceType ResourceType { get; set; } = ResourceType.Leaf;
	public override ResourceType GetResourceType() => ResourceType;
}