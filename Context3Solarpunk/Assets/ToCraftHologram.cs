using UnityEngine;
using UnityEngine.UI;

public class ToCraftHologram : Resource
{
	[field: SerializeField] protected override ResourceType ResourceType { get; set; }
	public override ResourceType GetResourceType() => ResourceType;

	[SerializeField] private Image progressImage;

	public void SetProgress(float progress)
	{
		progressImage.fillAmount = progress;
	}
}