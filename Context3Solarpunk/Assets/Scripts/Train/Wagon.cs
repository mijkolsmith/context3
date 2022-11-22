using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Wagon : MonoBehaviour
{
    [SerializeField] private Transform leftWagonAttachPoint;
    [SerializeField] private Transform rightWagonAttachPoint;

    [SerializeField] private bool hasPlayerInside = false;
    [SerializeField] private List<Vector3> trashcanSpawns;

    [SerializeField] private GameObject frontWall;
    [SerializeField] private Material[] materials;

    public Transform LeftWagonAttachPoint { get => leftWagonAttachPoint; private set => leftWagonAttachPoint = value; }
    public Transform RightWagonAttachPoint { get => rightWagonAttachPoint; private set => rightWagonAttachPoint = value; }
    public bool HasPlayerInside { get => hasPlayerInside; private set => hasPlayerInside = value; }

    private void Update()
    {
        Material[] newMaterials = frontWall.GetComponent<Renderer>().materials;
        if (HasPlayerInside)
        {
            newMaterials[0] = materials[2];
            newMaterials[1] = materials[3];
            frontWall.GetComponent<Renderer>().materials = newMaterials;
        } 
        else
        {
            newMaterials[0] = materials[0];
            newMaterials[1] = materials[1];
            frontWall.GetComponent<Renderer>().materials = newMaterials;
        }
    }

    //Yes this polls every frame whether or not the player is in it but it wouldn't work otherwise
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasPlayerInside = true;
        }
        else
        {
            hasPlayerInside = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasPlayerInside = false;
        }
    }
}