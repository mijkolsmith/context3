using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceButton : MonoBehaviour
{
	[SerializeField] private Button button;
	[SerializeField] private ResourceType resourceType;
	[SerializeField] private string[] resourceBaseTexts;
	[SerializeField] private TextMeshProUGUI resourceText;
	[SerializeField] private TextMeshProUGUI craftedText;
	private string craftedString;
	private bool crafted;

	/// <summary>
	/// Call the CraftingManager to craft a resource, and start the animation.
	/// </summary>
	public void Craft()
	{
		GameManager.Instance.CraftingManager.Craft(resourceType);
		crafted = true;
		UpdateUI();
	}

	/// <summary>
	/// Get the associated text object for the animation in the Start function.
	/// </summary>
	private void Start()
	{
		craftedString = craftedText?.text;
	}

	/// <summary>
	/// If crafted is true, play an animation in the Update function.
	/// </summary>
	private void Update()
	{
		//TODO: make the system so multiple can activate
		if (crafted)
		{
			craftedText.gameObject.SetActive(true);
			craftedText.transform.localPosition = new Vector3(craftedText.transform.localPosition.x, craftedText.transform.localPosition.y + Time.deltaTime * 30, craftedText.transform.localPosition.z);
			
			int alpha = 255 - (int)Mathf.Clamp(craftedText.transform.localPosition.y / 50f * 255f, 0f, 255f);
			alpha = alpha < 16 ? 16 : alpha;
			string alphaHex = alpha.ToString("X");
			craftedText.text = "<alpha=#" + alphaHex + ">" + craftedString;

			if (craftedText.transform.localPosition.y > 50)
			{
				crafted = false;
				craftedText.transform.localPosition = new Vector3(craftedText.transform.localPosition.x, 0, craftedText.transform.localPosition.z);
				craftedText.gameObject.SetActive(false);
			}
		}
	}

	/// <summary>
	/// Update the button UI.
	/// Make it interactable or invisible depending on whether it's craftable.
	/// </summary>
	public void UpdateUI()
	{
		//TODO: Do not display the button if the amount craftable is 0
		button.interactable = CanCraft();
	}

	/// <summary>
	/// Check whether the resource this button is for is craftable.
	/// </summary>
	/// <returns></returns>
	private bool CanCraft()
	{
		Dictionary<ResourceType, int> resourcesNeeded = GameManager.Instance.CraftingManager.CanCraft(resourceType, out bool canCraft);
		int i = 0;
		resourceText.text = string.Empty;
		foreach (ResourceType resource in resourcesNeeded.Keys)
		{
			resourceText.text += resourcesNeeded[resource] + resourceBaseTexts[i] + " ";
			i++;
		}
		return canCraft;
	}
}