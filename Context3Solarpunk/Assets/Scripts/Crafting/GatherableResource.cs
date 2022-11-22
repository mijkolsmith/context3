using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GatherableResource : Resource, IInteractable, IGatherable
{
	/// <summary>
	/// Add the GatherableResource to the inventory.
	/// </summary>
	public virtual void Gather()
	{
		GameManager.Instance.CraftingManager.AddResourceToInventory(GetResourceType());
	}

	/// <summary>
	/// Remove the trash from the count, and advance eventual quest tasks. 
	/// Deactivates quest objects in the questManager.
	/// </summary>
	public virtual void Interact()
	{
		Gather();
		GameManager.Instance.trashCount--;
        GameManager.Instance.QuestManager.AdvanceTasks();
    }
}