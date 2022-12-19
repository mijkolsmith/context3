public class Grass : Resource, ICraftable
{
	protected override ResourceType ResourceType { get; set; } = ResourceType.Grass;
	public override ResourceType GetResourceType() => ResourceType;
}