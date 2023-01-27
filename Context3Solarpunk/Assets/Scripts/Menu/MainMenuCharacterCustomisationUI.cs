using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class MainMenuCharacterCustomisationUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> characterCustomisationScreens = new List<GameObject>();

    public void SelectCharacterCustomisationScreen(int index)
    {
        for (int i = 0; i < characterCustomisationScreens.Count; i++)
        {
            if (i == index)
            {
                characterCustomisationScreens[i].SetActive(true);
            } 
            else
            {
                characterCustomisationScreens[i].SetActive(false);

            }
        }
    }
}
