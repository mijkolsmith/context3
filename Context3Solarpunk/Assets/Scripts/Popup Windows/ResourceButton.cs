using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;

/*public class ResourceButton : MonoBehaviour
{
	[SerializeField] private Button button;
	[SerializeField] private ResourceType resourceType;
	[SerializeField] private string[] resourceBaseTexts;
	[SerializeField] private TextMeshProUGUI resourceText;
	[SerializeField] private GameObject craftedObjectPrefab;
	[SerializeField, ReadOnly] private List<TextMeshProUGUI> craftedTextObjects;
	private string craftedText;
	private TextMeshProUGUI textObjectToRemove;

	/// <summary>
	/// Call the CraftingManager to craft a resource, and start the animation.
	/// </summary>
	public void Craft()
	{
		GameManager.Instance.CraftingManager.Craft(resourceType);
		craftedTextObjects.Add(Instantiate(craftedObjectPrefab, new Vector3(transform.position.x, transform.position.y + 10f, transform.position.z), Quaternion.identity, transform.parent).GetComponent<TextMeshProUGUI>());
		UpdateUI();
	}

	/// <summary>
	/// Check for destroyed objects and remove reference in the craftedTextObjects List in the Update method.
	/// </summary>
	private void Update()
	{
		craftedTextObjects.RemoveAll(x => x == null);
	}

	/// <summary>
	/// Update the button UI.
	/// Make it interactable or invisible depending on whether it's craftable.
	/// </summary>
	public void UpdateUI()
	{
		bool canCraft = CanCraft();
		button.interactable = canCraft;
	}

	/// <summary>
	/// Destroys all items in craftedTextObjects and clears the list.
	/// </summary>
	public void ClearCraftedTextObjects()
	{
		foreach(var craftedObject in craftedTextObjects)
		{
			Destroy(craftedObject.gameObject);
		}
		craftedTextObjects.Clear();
	}

	/// <summary>
	/// Check whether the resource this button is for is craftable.
	/// </summary>
	/// <returns></returns>
	private bool CanCraft()
	{
		Dictionary<ResourceType, int> resourcesNeeded = GameManager.Instance.CraftingManager.CanCraft(resourceType, out bool canCraft);

		bool displayButton = false;
		int i = 0;
		resourceText.text = string.Empty;
		foreach (ResourceType resource in resourcesNeeded.Keys)
		{
			resourceText.text += resourcesNeeded[resource] + resourceBaseTexts[i] + " ";
			i++;
			if (resourcesNeeded[resource] > 0)
			{
				displayButton = true;
			}
		}

		button.gameObject.SetActive(displayButton);

		return canCraft;
	}
}*/