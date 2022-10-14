using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Wagon : MonoBehaviour
{
    [SerializeField] private Transform leftWagonAttachPoint;
    [SerializeField] private Transform rightWagonAttachPoint;

    [SerializeField] private bool hasPlayerInside = false;
    [SerializeField] private Unit unit;
    [SerializeField] private List<Trash> trash;
    [SerializeField] private List<Vector3> trashcanSpawns;

    [SerializeField] private GameObject frontWall;

    public Transform LeftWagonAttachPoint { get => leftWagonAttachPoint; private set => leftWagonAttachPoint = value; }
    public Transform RightWagonAttachPoint { get => rightWagonAttachPoint; private set => rightWagonAttachPoint = value; }
    public bool HasPlayerInside { get => hasPlayerInside; private set => hasPlayerInside = value; }
    public Unit Unit { get => unit; private set => unit = value; }
    public GameObject FrontWall { get => frontWall; set => frontWall = value; }

    private void Update()
    {
        if (HasPlayerInside)
        {
            EnableFrontWallRenderers();
        } 
        else
        {
            DisableFrontWallRenderers();
        }
    }

    public void SpawnTrash()
    {
        Trash currentTrash = trash[Random.Range(0, trash.Count)];
        currentTrash.gameObject.SetActive(true);
        currentTrash.Spawn((TrashType)Random.Range(1, 4));
    }

    public void SpawnTrashcan(GameObject trashcanPrefab)
    {
        Vector3 pos = trashcanSpawns[Random.Range(0, trashcanSpawns.Count)];
        trashcanSpawns.Remove(pos);
        GameObject go = Instantiate(trashcanPrefab, pos, Quaternion.identity);
        go.transform.SetParent(transform, false);
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

    //repeating code, but wouldn't work in the time I had to fix this so this is how it be for now
    private void EnableFrontWallRenderers()
    {
        var wallRenderers = FrontWall.GetComponentsInChildren<MeshRenderer>();
        for (int j = 0; j < wallRenderers.Length; j++)
        {
            wallRenderers[j].enabled = false;
        }
    }

    private void DisableFrontWallRenderers()
    {
        var wallRenderers = FrontWall.GetComponentsInChildren<MeshRenderer>();
        for (int j = 0; j < wallRenderers.Length; j++)
        {
            wallRenderers[j].enabled = true;
        }
    }


}