public class LedLamp : Resource, ICraftable
{
	protected override ResourceType ResourceType { get; set; } = ResourceType.LedLamp;
	public override ResourceType GetResourceType() => ResourceType;
}