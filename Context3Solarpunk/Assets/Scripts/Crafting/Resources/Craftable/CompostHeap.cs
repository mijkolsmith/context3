public class CompostHeap : Resource, ICraftable
{
	protected override ResourceType ResourceType { get; set; } = ResourceType.CompostHeap;
	public override ResourceType GetResourceType() => ResourceType;
}