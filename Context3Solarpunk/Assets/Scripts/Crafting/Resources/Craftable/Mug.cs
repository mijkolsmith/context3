public class Mug : Resource, ICraftable
{
	protected override ResourceType ResourceType { get; set; } = ResourceType.Mug;
	public override ResourceType GetResourceType() => ResourceType;
}