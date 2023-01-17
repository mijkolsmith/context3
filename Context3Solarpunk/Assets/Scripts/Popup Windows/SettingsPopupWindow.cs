using UnityEngine;

public class SettingsPopupWindow : PopupWindow
{
	[field: SerializeField] protected override GameObject PopupWindowObject { get; set; }
	[field: SerializeField] protected override GameObject EndDragPanel { get; set; }
	public override PopupWindowType GetPopupWindowType() => PopupWindowType.Settings;
	public override RectTransform GetEndDragPanelRectTransform() => EndDragPanel.transform as RectTransform;

	/// <summary>
	/// The Toggle method gets called from the PopupWindow Class.
	/// It triggers the zoom-in animation to Dorien.
	/// Grabs all the UI elements.
	/// </summary>
	public override void Toggle()
	{
		if (PopupWindowObject.activeInHierarchy)
		{
			PopupWindowObject.SetActive(false);
			GameManager.Instance.UiManager.popupWindowOpenType = PopupWindowType.None;
			GameManager.Instance.SoundManager.PlayOneShotSound(SoundName.MENU_CLOSE);
		}
		else
		{
			PopupWindowObject.SetActive(true);
			GameManager.Instance.UiManager.popupWindowOpenType = PopupWindowType.Settings;
			GameManager.Instance.SoundManager.PlayOneShotSound(SoundName.MENU_OPEN);
		}
	}

	public override void UpdateUI()
	{
		
	}

	public void SaveQuit()
	{
		GameManager.Instance.SoundManager.PlayOneShotSound(SoundName.MENU_SELECT_02);
		Debug.Log("no functionality yet");
		//empty function
	}
}