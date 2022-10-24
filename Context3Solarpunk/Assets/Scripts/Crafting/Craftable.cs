using UnityEngine;

public abstract class Craftable : MonoBehaviour, ICraftable
{
    public abstract CraftableType GetCraftableType();
    public abstract void Craft();
}