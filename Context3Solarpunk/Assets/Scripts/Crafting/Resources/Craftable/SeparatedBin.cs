public class SeparatedBin : Resource, ICraftable
{
	protected override ResourceType ResourceType { get; set; } = ResourceType.SeparatedBin;
	public override ResourceType GetResourceType() => ResourceType;
}