using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerModelPreset", menuName = "PlayerCharacter")]
public class PlayerModelScriptableObject : ScriptableObject
{
    public bool isPantsCharacter;
    public string skinColorHex;
    public int hairIndex;
    public string hairColorHex;
    public string accessoryColorHex;

}
