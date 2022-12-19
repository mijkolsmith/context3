public class Can : GatherableResource
{
	protected override ResourceType ResourceType { get; set; } = ResourceType.Can;
	public override ResourceType GetResourceType() => ResourceType;
}