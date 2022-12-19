using UnityEngine;
using UnityEngine.UI;

public class CraftingUiElement : Resource
{
    [field: SerializeField] protected override ResourceType ResourceType { get; set; }
    public override ResourceType GetResourceType() => ResourceType;

    [SerializeField] private Image image;
    public bool activated = false;

    /// <summary>
    /// Change the alpha value to 255 in the image color.
    /// </summary>
    public void Activate()
	{
        image.color = new Color(image.color.r, image.color.g, image.color.b, 255);
        activated = true;
	}
}