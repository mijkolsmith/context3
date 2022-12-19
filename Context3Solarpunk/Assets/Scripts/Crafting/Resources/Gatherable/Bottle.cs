public class Bottle : GatherableResource
{
	protected override ResourceType ResourceType { get; set; } = ResourceType.Bottle;
	public override ResourceType GetResourceType() => ResourceType;
}