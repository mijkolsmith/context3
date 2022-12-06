using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using NaughtyAttributes;

public class InventoryUiElement : Resource, IInitializePotentialDragHandler, IDragHandler
{
	[SerializeField] private ResourceType resourceType;
	[SerializeField] public TextMeshProUGUI amountText;
	public override ResourceType GetResourceType() => resourceType;
	[SerializeField] private Image image;
	[HideInInspector] public CraftingPopupWindow craftingPopupWindow;
	[SerializeField, ReadOnly] private int count;

	/// <summary>
	/// Update the inventory UI.
	/// Update it's amount and make it invisible if it's 0.
	/// </summary>
	public void UpdateUI()
	{
		count = GameManager.Instance.CraftingManager.Resources.Where(x => x == resourceType).Count();
		amountText.text = count.ToString();
		gameObject.SetActive(count > 0);
	}

	/// <summary>
	/// Unity default interface function to check if a drag is initialized.
	/// Change the eventData with a new initialized object which is draggable.
	/// </summary>
	/// <param name="eventData"></param>
	public void OnInitializePotentialDrag(PointerEventData eventData)
	{
		if (craftingPopupWindow != null && count > 0)
		{
			eventData.pointerDrag = craftingPopupWindow.GetComponent<CraftingPopupWindow>().SpawnDraggableObject(transform.position, resourceType, image.sprite);
		}
	}

	/// <summary>
	/// Unity default interface function to check if a drag is happening.
	/// Empty function but the dragging does not work without calling it.
	/// </summary>
	/// <param name="eventData"></param>
	public void OnDrag(PointerEventData eventData)
	{
		//empty
	}

	/// <summary>
	/// Temporarily change the UI to display one less element.
	/// </summary>
	public void TemporarilyRemove()
	{
		count--;
		amountText.text = count.ToString();
		gameObject.SetActive(count > 0);
	}
}