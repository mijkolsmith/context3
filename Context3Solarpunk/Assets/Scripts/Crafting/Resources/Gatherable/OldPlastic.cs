public class OldPlastic : GatherableResource
{
	protected override ResourceType ResourceType { get; set; } = ResourceType.OldPlastic;
	public override ResourceType GetResourceType() => ResourceType;
}