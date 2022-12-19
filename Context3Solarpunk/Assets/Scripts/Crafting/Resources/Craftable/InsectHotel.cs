public class InsectHotel : Resource, ICraftable
{
	protected override ResourceType ResourceType { get; set; } = ResourceType.InsectHotel;
	public override ResourceType GetResourceType() => ResourceType;
}