using NaughtyAttributes;
using UnityEngine;

public class RecycleBin : MonoBehaviour, IInteractable
{
	[SerializeField, ReadOnly] private Outline objectOutline;
	private bool highlighting = false;
	private float recycleBinOpenDelay = 10f;

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
		if (recycleBinOpenDelay > 0) recycleBinOpenDelay -= Time.deltaTime;
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
		if (recycleBinOpenDelay <= 0)
		{
			GameManager.Instance.UiManager.TogglePopupWindow(PopupWindowType.RecycleBin);
			GameManager.Instance.QuestManager.AdvanceTasks(this);
		}
	}

	/// <summary>
	/// Activate the highlight outline.
	/// </summary>
	/// <param name="color"></param>
	public void Highlight(Color color)
	{
		if (recycleBinOpenDelay <= 0)
		{
			objectOutline.OutlineWidth = 5f;
			objectOutline.OutlineColor = color;
			highlighting = true;
		}
	}
}