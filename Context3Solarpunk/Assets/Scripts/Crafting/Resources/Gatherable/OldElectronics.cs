public class OldElectronics : GatherableResource
{
	protected override ResourceType ResourceType { get; set; } = ResourceType.OldElectronics;
	public override ResourceType GetResourceType() => ResourceType;
}