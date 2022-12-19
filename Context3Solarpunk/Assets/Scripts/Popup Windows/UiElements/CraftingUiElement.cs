using UnityEngine;
using UnityEngine.UI;

public class CraftingUiElement : Resource
{
    [field: SerializeField] protected override ResourceType ResourceType { get; set; }
    public override ResourceType GetResourceType() => ResourceType;

    [SerializeField] protected Image Image;
    public bool activated = false;

    /// <summary>
    /// Change the alpha value to 255 in the image color.
    /// </summary>
    public void Activate()
	{
        Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, 255);
        activated = true;
	}
}