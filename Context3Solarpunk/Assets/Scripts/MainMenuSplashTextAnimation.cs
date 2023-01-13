using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMenuSplashTextAnimation : MonoBehaviour
{
    [SerializeField] private GameObject splashTextObject;
    [SerializeField] private AnimationCurve scaleCurve;

    [SerializeField] private float desiredScale = 1;
    [SerializeField] private float animationTime = 1;
    private float startTime;

    void Awake()
    {
        startTime = Time.time;
        transform.localScale = Vector3.zero;
    }

    void Update()
    {
        float currentTime = Time.time - startTime;
        float t = currentTime / animationTime;
        float scale = scaleCurve.Evaluate(t) * desiredScale;
        splashTextObject.transform.localScale = new Vector3(scale, scale, scale);

        if (t >= 1)
        {
            enabled = false;
        }
    }
}