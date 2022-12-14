using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftedObject : Resource
{
	[field: SerializeField] protected override ResourceType ResourceType { get; set; }
	public override ResourceType GetResourceType() => ResourceType;

	[SerializeField] private GameObject model;
	[SerializeField] private GameObject oldModel;
	[SerializeField] private GameObject modelDorien;
	[SerializeField] private GameObject modelHologram;

	public void ActivateHologram()
	{
		if (oldModel != null) oldModel.SetActive(false);
		modelDorien.SetActive(true);
		modelHologram.SetActive(true);
	}

	public void Activate()
	{
		model.SetActive(true);

		modelDorien.SetActive(false);
		modelHologram.SetActive(false);

		GameManager.Instance.QuestManager.AdvanceBuildTask(this);
	}
}