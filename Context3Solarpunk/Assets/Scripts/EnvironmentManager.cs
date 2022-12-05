using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


public class EnvironmentManager : MonoBehaviour
{


    [SerializeField] private List<Environment> environments;
    [Range(0, 10), SerializeField] private int progress = 0;


    private bool validated = false;

    public int Progress { 
        get => progress;
        set
        {
            progress = value;
            AdvanceScene();
        }
    }

    private void Update()
    {
        if (validated)
        {
            AdvanceScene();
            validated = false;
        }
    }

    void OnValidate()
    {
        //Dit voelt zeer omslachtig maar dit voorkomt dat de console vol wordt gespamd met 600 warnings, het werkt hierdoor alleen niet buiten playmode
        validated = true;
    }


    private void AdvanceScene()
    {
        for (int i = 0; i < environments.Count; i++)
        {
            if (i == Progress)
            {
                for (int j = 0; j < environments[i].objects.Count; j++)
                {
                    environments[i].objects[j].SetActive(true);
                    RenderSettings.fog = environments[i].foggy;
                }
            }
            else
            {
                for (int j = 0; j < environments[i].objects.Count; j++)
                {
                    environments[i].objects[j].SetActive(false);
                }
            }
        }
    }
}