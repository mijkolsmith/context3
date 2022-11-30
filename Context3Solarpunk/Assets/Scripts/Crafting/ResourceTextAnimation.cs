using UnityEngine;
using TMPro;

public class ResourceTextAnimation : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI textComponent;
	private string text;
	private float startHeight;
	[SerializeField] private ResourceType resourceType;
	public ResourceType GetResourceType() => resourceType;

	/// <summary>
	/// Get the text from the component in the Start method.
	/// </summary>
	private void Start()
	{
		text = textComponent.text;
		startHeight = transform.localPosition.y;
	}

	/// <summary>
	/// Play the animation in the Update method.
	/// </summary>
	private void Update()
    {
		transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + Time.deltaTime * 30, transform.localPosition.z);

		// Apply alpha coloring
		int alpha = 255 - (int)Mathf.Clamp((transform.localPosition.y - startHeight) / 50 * 255, 0f, 255f);
		alpha = alpha < 16 ? 16 : alpha;
		string alphaHex = alpha.ToString("X");
		textComponent.text = "<alpha=#" + alphaHex + ">" + text;

		if (transform.localPosition.y > startHeight + 50)
		{
			Destroy(gameObject);
		}
	}
}