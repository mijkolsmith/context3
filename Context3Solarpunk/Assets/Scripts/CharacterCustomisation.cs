using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class CharacterCustomisation : MonoBehaviour
{
    [Header("Skin Related Variables")]
    [SerializeField] private List<GameObject> skinColourObjects = new List<GameObject>();
    [SerializeField] private Color skinColor;
    [HorizontalLine]
    [Header("HairStyle related Variables")]
    [SerializeField] private Color accessoryColor;
    [SerializeField] private List<GameObject> hairStyles = new List<GameObject>();
    [SerializeField] private GameObject hairParent;
    [SerializeField] private GameObject selectedHairStyle;

    [SerializeField] private Material hairMaterial;
    [SerializeField] private Material accessoryMaterial;



    void Awake()
    {
        SetHairStyle(0);
    }

    public void SetSkinColorByHex(string hexColor)
    {
        foreach (GameObject go in skinColourObjects)
        {
            Color newCol;
            Renderer skinRenderer = go.GetComponent<Renderer>();

            if (ColorUtility.TryParseHtmlString(hexColor, out newCol))
            {
                skinRenderer.material.color = newCol;
            }
        }
    }


    public void SetHairStyle(int hairStyleIndex)
    {
        for (int i = 0; i < hairStyles.Count; i++)
        {
            if (i == hairStyleIndex)
            {
                Destroy(selectedHairStyle);
                selectedHairStyle = Instantiate(hairStyles[i], hairParent.transform);
            }
        }
    }

    public void SetHairColour(string hexColor)
    {
        Color newCol;
        Renderer r = selectedHairStyle.GetComponentInChildren<Renderer>();

        if (ColorUtility.TryParseHtmlString(hexColor, out newCol))
        {
            hairMaterial.color = newCol;
            accessoryMaterial.color = newCol;
        }
    }

}
