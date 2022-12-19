using UnityEngine;

public class RecyclingMachine : MonoBehaviour, IInteractable
{
	private Outline objectOutline;
	private bool highlighting = false;

	/// <summary>
	/// Assign Outline component in the start method.
	/// </summary>
	private void Start()
	{
		objectOutline = GetComponent<Outline>();
	}

	/// <summary>
	/// Reset the outline if it's not reactivated each frame.
	/// </summary>
	private void Update()
	{
		if (!highlighting)
		{
			objectOutline.OutlineWidth = 0f;
		}
		highlighting = false;
	}

	/// <summary>
	/// Open the crafting popup window.
	/// </summary>
	public void Interact()
	{
		GameManager.Instance.UiManager.TogglePopupWindow(PopupWindowType.Crafting);
		GameManager.Instance.QuestManager.AdvanceTasks(this);
	}

	/// <summary>
	/// Activate the highlight outline.
	/// </summary>
	/// <param name="color"></param>
	public void Highlight(Color color)
	{
		objectOutline.OutlineWidth = 5f;
		objectOutline.OutlineColor = color;
		highlighting = true;
	}
}