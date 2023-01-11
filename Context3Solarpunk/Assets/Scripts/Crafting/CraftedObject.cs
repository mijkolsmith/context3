using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftedObject : Resource
{
	[field: SerializeField] protected override ResourceType ResourceType { get; set; }
	public override ResourceType GetResourceType() => ResourceType;

	[SerializeField] private GameObject model;

	public void Activate()
	{
		model.SetActive(true);
        GameManager.Instance.QuestManager.AdvanceBuildTask(model);
	}
}