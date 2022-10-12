using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundObject : MonoBehaviour
{
    [SerializeField] private GameObject leftAnchor;
    [SerializeField] private GameObject rightAnchor;

    public GameObject LeftAnchor { get => leftAnchor; set => leftAnchor = value; }
    public GameObject RightAnchor { get => rightAnchor; set => rightAnchor = value; }


}
