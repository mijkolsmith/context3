using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerManager : MonoBehaviour
{

    [Header("SpawnPosition"),SerializeField] private Transform spawnPosition;
    //[SerializeField] private PlayerModelScriptableObject playerAsset;

    [SerializeField, ReadOnly] private GameObject playerObject;
    [SerializeField, ReadOnly] private GameObject selectedHairStyle;

    [SerializeField] private GameObject pantsPlayerPrefab;
    [SerializeField] private GameObject skirtPlayerPrefab;
    [SerializeField] private Transform companionTransform;

    [SerializeField] private Material hairMaterial;
    [SerializeField] private Material accessoryMaterial;

    [SerializeField] private List<GameObject> hairStylePrefabs = new List<GameObject>();

    private PlayerModel playerModel;

    public GameObject PlayerObject { get => playerObject; set => playerObject = value; }

    public void SpawnPlayer()
    {
        bool isPantsBool = PlayerPrefs.GetInt("_isPantsCharacter") != 0;
        Debug.Log(isPantsBool);
        PlayerObject = isPantsBool ? Instantiate(pantsPlayerPrefab, spawnPosition) : Instantiate(skirtPlayerPrefab, spawnPosition);

        playerModel = PlayerObject.GetComponent<PlayerModel>();
        companionTransform = playerObject.GetComponent<PlayerControllerPointClick>().CompanionPositionGameObject.transform;
        SetSkinColor(PlayerPrefs.GetString("_skinColorHex"));
        SetHair(PlayerPrefs.GetInt("_hairIndex"),PlayerPrefs.GetString("_hairColorHex"));
    }
    public void SetSkinColor(string hexColor)
    {
        Color newCol;
        Renderer skinRenderer = playerModel.skinObject.GetComponent<Renderer>();
        if (ColorUtility.TryParseHtmlString(hexColor, out newCol))
        {
            skinRenderer.material.color = newCol;
        }
    }
    public void SetHair(int hairIndex, string hairColorHex)
    {
        //Set hair style
        for (int i = 0; i < hairStylePrefabs.Count; i++)
        {
            if (i == hairIndex)
            {
                Destroy(selectedHairStyle);
                selectedHairStyle = Instantiate(hairStylePrefabs[i], playerModel.hairParent.transform);
            }
        }

        //Set Hair color and accessory color
        Color hairColor;
        if (ColorUtility.TryParseHtmlString(hairColorHex, out hairColor))
        {
            hairMaterial.color = hairColor;
            accessoryMaterial.color = Color.cyan;
        }
    }
}
