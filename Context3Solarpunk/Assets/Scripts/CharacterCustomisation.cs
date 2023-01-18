using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class CharacterCustomisation : MonoBehaviour
{
    [Header("Body Clothing related Variables")]
    [SerializeField] private Transform characterPosition;
    [SerializeField, ReadOnly] private GameObject selectedCharacter;

    [Header("Clothing Related Prefabs")]
    [SerializeField] private GameObject pantsCharacterPrefab;
    [SerializeField] private GameObject skirtCharacterPrefab;



    [Header("Skin Related Variables")]
    [SerializeField] private GameObject skinColourObject;
    [HorizontalLine]
    [Header("HairStyle related Variables")]
    [SerializeField] private List<GameObject> hairStyles = new List<GameObject>();
    [SerializeField] private GameObject hairParent;
    [SerializeField] private GameObject selectedHairStyle;

    [SerializeField] private Material hairMaterial;
    [SerializeField] private Material accessoryMaterial;



    [Header("Preset info")]
    [SerializeField, ReadOnly] private bool isPantsCharacter;
    [SerializeField, ReadOnly] private string skinColorHex = "#FFF3EB";
    [SerializeField, ReadOnly] private int hairIndex = 0;
    [SerializeField, ReadOnly] private string hairColorHex = "#E4DFD9";
    [SerializeField, ReadOnly] private string accessoryColorHex = "#91DDE3";

    [SerializeField] private PlayerModelScriptableObject playerAsset;



    void Awake()
    {
        UpdateClothing(true);
        SetHairStyle(0);
    }

    public void SetSkinColorByHex(string hexColor)
    {

        Color newCol;
        Renderer skinRenderer = skinColourObject.GetComponent<Renderer>();

        if (ColorUtility.TryParseHtmlString(hexColor, out newCol))
        {
            skinRenderer.material.color = newCol;
            skinColorHex = hexColor;
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
        hairIndex = hairStyleIndex;
    }

    public void SetHairColour(string hexColor)
    {
        Color newCol;

        if (ColorUtility.TryParseHtmlString(hexColor, out newCol))
        {
            hairMaterial.color = newCol;
            accessoryMaterial.color = Color.cyan;
        }
        hairColorHex = hexColor;
    }

    public void UpdateClothing(bool pantsCharacter)
    {
        if (pantsCharacter)
        {
            isPantsCharacter = true;
            Destroy(selectedCharacter);
            selectedCharacter = Instantiate(pantsCharacterPrefab, characterPosition);
        }
        else
        {
            isPantsCharacter = false;
            Destroy(selectedCharacter);
            selectedCharacter = Instantiate(skirtCharacterPrefab, characterPosition);
        }
        hairParent = selectedCharacter.GetComponent<PlayerModel>().hairParent;
        skinColourObject = selectedCharacter.GetComponent<PlayerModel>().skinObject;
        SetHairStyle(hairIndex);
        SetHairColour(hairColorHex);
        SetSkinColorByHex(skinColorHex);
    }

    public void UpdateAsset()
    {
        playerAsset.isPantsCharacter = isPantsCharacter;
        playerAsset.skinColorHex = skinColorHex;
        playerAsset.hairIndex = hairIndex;
        playerAsset.hairColorHex = hairColorHex;
        playerAsset.accessoryColorHex = accessoryColorHex;
    }

}
