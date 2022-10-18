using System;
using UnityEngine;

[Serializable]
public abstract class PopupWindow : MonoBehaviour, IPopupWindow
{
	public abstract PopupWindowType GetPopupWindowType();
	public abstract void Open();
}