public class SunPanel : Resource, ICraftable
{
	protected override ResourceType ResourceType { get; set; } = ResourceType.SunPanel;
	public override ResourceType GetResourceType() => ResourceType;
}