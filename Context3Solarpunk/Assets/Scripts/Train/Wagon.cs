using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wagon : MonoBehaviour
{
    [SerializeField] private Transform leftWagonAttachPoint;
    [SerializeField] private Transform rightWagonAttachPoint;

    [SerializeField] private bool hasPlayerInside = false;
    [SerializeField] private Unit unit;
    [SerializeField] private List<Trash> trash;

    [SerializeField] private GameObject frontWall;

    public Transform LeftWagonAttachPoint { get => leftWagonAttachPoint; private set => leftWagonAttachPoint = value; }
    public Transform RightWagonAttachPoint { get => rightWagonAttachPoint; private set => rightWagonAttachPoint = value; }
    public bool HasPlayerInside { get => hasPlayerInside; private set => hasPlayerInside = value; }
    public Unit Unit { get => unit; private set => unit = value; }
    public GameObject FrontWall { get => frontWall; set => frontWall = value; }

    public void SpawnTrash()
    {
        Trash currentTrash = trash[Random.Range(0, trash.Count)];
        currentTrash.gameObject.SetActive(true);
        currentTrash.Spawn((TrashType)Random.Range(1,4));
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