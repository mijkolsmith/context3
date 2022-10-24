using System.Collections.Generic;

public interface ICraftable
{
    public abstract void Craft();
    public abstract Dictionary<ResourceType, int> GetResourcesNeeded();
}