public class VegetableGarden : Resource, ICraftable
{
	protected override ResourceType ResourceType { get; set; } = ResourceType.VegetableGarden;
	public override ResourceType GetResourceType() => ResourceType;
}