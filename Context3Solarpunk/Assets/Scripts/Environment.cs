using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Environment
{
    public string name;
    public bool foggy;
    public Material skyboxMaterial;
    public List<GameObject> objects;
}
