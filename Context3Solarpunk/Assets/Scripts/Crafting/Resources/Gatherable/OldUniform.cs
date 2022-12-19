public class OldUniform : GatherableResource
{
	protected override ResourceType ResourceType { get; set; } = ResourceType.OldUniform;
	public override ResourceType GetResourceType() => ResourceType;
}