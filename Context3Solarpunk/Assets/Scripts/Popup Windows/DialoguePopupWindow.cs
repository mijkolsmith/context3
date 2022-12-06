using UnityEngine;

public class DialoguePopupWindow : PopupWindow
{
	[field: SerializeField] protected override GameObject PopupWindowObject { get; set; }
	public override PopupWindowType GetPopupWindowType() => PopupWindowType.Dialogue;

	public override void Toggle()
	{
		if (PopupWindowObject.activeInHierarchy)
		{
			PopupWindowObject.SetActive(false);
			GameManager.Instance.UiManager.popupWindowOpenType = PopupWindowType.None;
		}
		else
		{
			PopupWindowObject.SetActive(true);
			GameManager.Instance.UiManager.popupWindowOpenType = PopupWindowType.Dialogue;
		}
	}
}