using System;
using UnityEngine;

[Serializable]
public abstract class PopupWindow : MonoBehaviour, IPopupWindow
{
	protected abstract GameObject PopupWindowObject { get; set; }
	public abstract PopupWindowType GetPopupWindowType();
	public abstract void Toggle();
}