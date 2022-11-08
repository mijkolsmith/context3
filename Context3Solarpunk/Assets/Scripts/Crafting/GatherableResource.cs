using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GatherableResource : Resource, IInteractable, IGatherable
{
	public virtual void Gather()
	{
		GameManager.Instance.CraftingManager.AddResourceToInventory(GetResourceType());
	}

	public virtual void Interact()
	{
		Gather();
		GameManager.Instance.trashCount--;
		Destroy(gameObject);
	}
}