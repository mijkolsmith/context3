using UnityEngine;

public class PopupWindowComponent : MonoBehaviour, IInteractable
{
    public PopupWindowType popupWindow;

	public void Highlight(Color color)
	{

	}

	public void Interact()
	{
		GameManager.Instance.QuestManager.AdvanceTasks(this);
	}
}