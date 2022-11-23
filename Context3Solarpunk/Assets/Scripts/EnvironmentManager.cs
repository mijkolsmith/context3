using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


public class EnvironmentManager : MonoBehaviour
{


    [SerializeField] private List<Environment> environments;
    [Range(0,10),SerializeField] private int progress = 0;



    private void OnValidate()
    {
        AdvanceScene();
    }

    private void AdvanceScene()
    {
        for (int i = 0; i < environments.Count; i++)
        {
            if (progress < i)
            {
                for (int j = 0; j < environments[i].objects.Count; j++)
                {
                    environments[i].objects[j].SetActive(false);
                }
            }
            else
            {
                for (int j = 0; j < environments[i].objects.Count; j++)
                {
                    environments[i].objects[j].SetActive(true);
                }
            }
        }
    }
}