using UnityEngine;
using NaughtyAttributes;

public class Unit : MonoBehaviour, IInteractable
{
    [SerializeField] private Material unitWorkingMaterial;
    [SerializeField] private Material unitBrokenMaterial;
	[SerializeField, ReadOnly] public MeshRenderer meshRenderer;
	[SerializeField, ReadOnly] protected bool broken;
	public bool Broken { get => broken; protected set => broken = value; }
	[SerializeField] private int breakHappinessValue = -5;
	//[SerializeField] private int fixHappinessValue = 25; moved to unitPopupWindow
	[SerializeField] private int unit;

	private void Start()
	{
		meshRenderer = GetComponent<MeshRenderer>();
	}

	public void Interact()
	{
		Fix();
	}

	public void Break()
	{
		if (Broken == false)
		{
			meshRenderer.material = unitBrokenMaterial;
			GameManager.Instance.ToggleIndicator(unit);
			Broken = true;
		}
		GameManager.Instance.ChangePassengerHappiness(breakHappinessValue);
	}

	private void Fix()
	{
		if (Broken == true)
		{
			meshRenderer.material = unitWorkingMaterial;
			GameManager.Instance.ToggleIndicator(unit);
			GameManager.Instance.TogglePopupWindow(PopupWindowType.Unit);
		}
		Broken = false;
	}
}