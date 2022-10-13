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
	[SerializeField] private int fixHappinessValue = 25;

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
		meshRenderer.material = unitBrokenMaterial;
		Broken = true;
		GameManager.Instance.ChangePassengerHappiness(breakHappinessValue);
	}

	private void Fix()
	{
		meshRenderer.material = unitWorkingMaterial;
		if (Broken == true) GameManager.Instance.ChangePassengerHappiness(fixHappinessValue);
		Broken = false;
	}
}