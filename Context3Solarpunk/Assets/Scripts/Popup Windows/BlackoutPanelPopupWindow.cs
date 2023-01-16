using UnityEngine;

public class BlackoutPanelPopupWindow : PopupWindow
{
    [field: SerializeField] protected override GameObject PopupWindowObject { get; set; }
    [field: SerializeField] protected override GameObject EndDragPanel { get; set; }
    public override PopupWindowType GetPopupWindowType() => PopupWindowType.BlackoutPanel;
    public override RectTransform GetEndDragPanelRectTransform() => EndDragPanel.transform as RectTransform;

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
            GameManager.Instance.UiManager.popupWindowOpenType = PopupWindowType.BlackoutPanel;
        }
    }

	public override void UpdateUI()
	{
		
	}
}