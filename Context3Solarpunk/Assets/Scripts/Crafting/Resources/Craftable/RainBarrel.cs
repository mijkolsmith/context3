public class RainBarrel : Resource, ICraftable
{
	protected override ResourceType ResourceType { get; set; } = ResourceType.RainBarrel;
	public override ResourceType GetResourceType() => ResourceType;
}