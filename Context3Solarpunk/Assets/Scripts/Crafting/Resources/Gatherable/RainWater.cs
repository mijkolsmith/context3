public class RainWater : GatherableResource
{
	protected override ResourceType ResourceType { get; set; } = ResourceType.RainWater;
	public override ResourceType GetResourceType() => ResourceType;
}