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
    [SerializeField] private List<GameObject> hairStyles = new List<GameObject>();

    void Awake()
    {

    }

    public void SetSkinColour(Color color)
    {
        foreach (GameObject go in skinColourObjects)
        {
            Renderer skinRenderer = go.GetComponent<Renderer>();
            skinRenderer.material.color = color;

        }
    }

    public void SetSkinColorByHex(string hexColor)
    {
        Debug.Log("Buttonpresscalled");
        foreach (GameObject go in skinColourObjects)
        {
            Debug.Log(go.name);
            Color newCol;
            Renderer skinRenderer = go.GetComponent<Renderer>();

            if (ColorUtility.TryParseHtmlString(hexColor, out newCol))
            {
                Debug.Log(newCol);
                skinRenderer.material.color = newCol;
            }
        }
    }

    public void DebugColour(Color parColor)
    {
        Color color = parColor;
        string message = "Color " + 1 + " tested.";

        Debug.Log(string.Format("<color=#{0:X2}{1:X2}{2:X2}>{3}</color>", (byte)(color.r * 255f), (byte)(color.g * 255f), (byte)(color.b * 255f), message));
    }
}
