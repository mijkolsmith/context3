


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;

public class ResourceButton : MonoBehaviour
{
	[SerializeField] private Button button;
	[SerializeField] private ResourceType resourceType;
	[SerializeField] private string[] resourceBaseTexts;
	[SerializeField] private TextMeshProUGUI resourceText;
	[SerializeField] private TextMeshProUGUI craftedText;
	private string craftedString;
	private bool crafted;

	public void Craft()
	{
		GameManager.Instance.CraftingManager.Craft(resourceType);
		crafted = true;
		UpdateUI();
	}

	private void Start()
	{
		craftedString = craftedText?.text;
	}

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

	public void UpdateUI()
	{
		button.interactable = CanCraft();
	}

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