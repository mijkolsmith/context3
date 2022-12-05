using UnityEngine;
using UnityEngine.UI;

public class CraftingUiElement : MonoBehaviour
{
    [SerializeField] private Image image;
    public bool activated = false;
    [SerializeField] private ResourceType resourceType;
    public ResourceType GetResourceType() => resourceType;

    /// <summary>
    /// Change the alpha value to 255 in the image color.
    /// </summary>
    public void Activate()
	{
        image.color = new Color(image.color.r, image.color.g, image.color.b, 255);
        activated = true;
	}
}