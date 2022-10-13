using UnityEngine;

[System.Serializable]
public struct WagonReference
{
    public int wagonNmbr;
    public GameObject wagonObject;
    public WagonType wagonType;
    [HideInInspector] public Wagon wagon;
}