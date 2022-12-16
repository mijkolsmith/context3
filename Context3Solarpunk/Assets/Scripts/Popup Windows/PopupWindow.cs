using System;
using UnityEngine;

[Serializable]
public abstract class PopupWindow : MonoBehaviour, IPopupWindow
{
	protected abstract GameObject PopupWindowObject { get; set; }
	protected abstract GameObject EndDragPanel { get; set; }
	public abstract void UpdateUI();
	public abstract void Toggle();
	public abstract PopupWindowType GetPopupWindowType();
	public abstract RectTransform GetEndDragPanelRectTransform();
}