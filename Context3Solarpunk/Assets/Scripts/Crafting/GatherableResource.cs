using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GatherableResource : Resource, IInteractable, IGatherable
{
	public virtual void Gather()
	{
		GameManager.Instance.CraftingManager.AddResource(this);
	}

	public virtual void Interact()
	{
		Gather();
	}
}