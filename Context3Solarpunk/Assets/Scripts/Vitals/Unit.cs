using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Unit : MonoBehaviour, IInteractable
{
    [SerializeField] private Material unitWorkingMaterial;
    [SerializeField] private Material unitBrokenMaterial;
	[SerializeField, ReadOnly] public MeshRenderer meshRenderer;
	[SerializeField] protected bool broken;
	public bool Broken { get => broken; protected set => broken = value; }

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
		GameManager.Instance.ChangePassengerHappiness(-5);
	}

	private void Fix()
	{
		meshRenderer.material = unitWorkingMaterial;
		if (Broken == true) GameManager.Instance.ChangePassengerHappiness(20);
		Broken = false;
		
	}
}