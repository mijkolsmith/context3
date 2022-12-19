public class Uniform : Resource, ICraftable
{
	protected override ResourceType ResourceType { get; set; } = ResourceType.Uniform;
	public override ResourceType GetResourceType() => ResourceType;
}