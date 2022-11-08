using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField] private GameObject blackoutSquare;

    [SerializeField] private int fadeSpeed = 5;

    [ContextMenu("FadeToBlack")]
    public void FadeToBlack()
    {
        StartCoroutine(FadeBlackout(true));
    }

    [ContextMenu("FadeToTransparent")]
    public void FadeToTransparent()
    {
        StartCoroutine(FadeBlackout(false));
    }


    public IEnumerator FadeBlackout(bool fadeToBlack)
    {
        Color objectColor = blackoutSquare.GetComponent<Image>().color;
        float fadeAmount;

        if (fadeToBlack)
        {
            while (blackoutSquare.GetComponent<Image>().color.a < 1)
            {

                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackoutSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
        else
        {
            while (blackoutSquare.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackoutSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
    }
}
