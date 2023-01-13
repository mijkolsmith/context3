using UnityEngine;
using NaughtyAttributes;

public class CraftedObject : Resource
{
	[field: SerializeField] protected override ResourceType ResourceType { get; set; }
	public override ResourceType GetResourceType() => ResourceType;

	[SerializeField] private GameObject model;
	[SerializeField, ShowIf("GetResourceType", ResourceType.LedLamp)] private GameObject oldModel;
	[SerializeField, ShowIf("GetResourceType", ResourceType.LedLamp)] private GameObject darkness;
	[SerializeField] private GameObject modelDorien;
	[SerializeField] private GameObject modelHologram;

	public void ActivateHologram()
	{
		oldModel.SetActive(false);
		modelDorien.SetActive(true);
		modelHologram.SetActive(true);
	}

	public void Activate()
	{
		model.SetActive(true);

		darkness.SetActive(false);
		modelDorien.SetActive(false);
		modelHologram.SetActive(false);

		GameManager.Instance.QuestManager.AdvanceBuildTask(this);
	}
}