using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

/// <summary>
/// Shows what state the sequence is in and handles 
/// </summary>
public enum sequenceState
{
    notInSequence = 0,
    sequenceFadingIn = 1,
    SequenceCenter = 2,
    SequenceFadingOut = 3
}

public class SequenceManager : MonoBehaviour
{
    [SerializeField] private bool timeTravelling = false;

    [SerializeField] private int currentYear = 2082;

    [SerializeField] private sequenceState currentSequenceState = sequenceState.notInSequence;
    [SerializeField] private GameObject blackoutSquareUI;
    [SerializeField] private float transitionSpeed = 0.0025f;
    [SerializeField] private float timeTravelSpeed = 15f;

    [SerializeField] private float countDuration = 2.5f;
    [SerializeField] private float fadeDuration = 1.5f;

    private float fadeSpeed = 5f;

    [SerializeField, ReadOnly] private int targetYear;

    private float t; //a timer

    /// <summary>
    /// Preset method that can be executed in the inspector to go to any year with param
    /// </summary>
    /// <param name="year"></param>
    public void TimeTravel()
    {
        if (!timeTravelling)
        {
            if (GameManager.Instance.EnvironmentManager.InThePast)
            {
                TimeTravelToTheFuture();
            }
            else
            {
                TimeTravelToThePast();
            }
        }
    }

    /// <summary>
    /// Preset method that can be executed in the inspector to go to 2082 with sequence
    /// </summary>
    [ContextMenu("Run travel sequence to the future")]
    public void TimeTravelToTheFuture()
    {
        targetYear = 2082;
        timeTravelling = true;
        currentSequenceState = sequenceState.sequenceFadingIn;
    }

    /// <summary>
    /// Preset method that can be executed in the inspector to go to 2022 with sequence
    /// </summary>
    [ContextMenu("Run travel sequence to the past")]
    public void TimeTravelToThePast()
    {
        targetYear = 2022;
        timeTravelling = true;
        currentSequenceState = sequenceState.sequenceFadingIn;
    }

    private void Update()
    {
        if (timeTravelling)
        {
            switch (currentSequenceState)
            {
                case sequenceState.notInSequence:
                    break;
                case sequenceState.sequenceFadingIn:
                    StartCoroutine(Fade(true));
                    break;
                case sequenceState.SequenceCenter:
                    StartCoroutine(ChangeYearAmount(targetYear));
                    if (GameManager.Instance.EnvironmentManager.InThePast)
                    {
                        GameManager.Instance.EnvironmentManager.InThePast = false;
                        GameManager.Instance.EnvironmentManager.Progress = 0;
                    } 
                    else
                    {
                        GameManager.Instance.EnvironmentManager.InThePast = true;
                        GameManager.Instance.EnvironmentManager.Progress = 1;
                    }
                    break;
                case sequenceState.SequenceFadingOut:
                    StartCoroutine(Fade(false));
                    timeTravelling = false;
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// This either fades or unfades the screen depending on the fadeIn param
    /// </summary>
    /// <param name="fadeIn"></param>
    /// <returns></returns>
    private IEnumerator Fade(bool fadeIn)
    {
        Image fadeImage = blackoutSquareUI.GetComponent<Image>();

        // get the current color of the image
        Color currentColor = fadeImage.color;

        // create a timer
        float t = 0f;

        // if fading in
        if (fadeIn)
        {
            // while the timer is less than the duration
            while (t < fadeDuration)
            {
                // set the alpha value of the image based on the timer
                Color objectColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(0f, 1f, t / fadeDuration));

                GameManager.Instance.UiManager.CurrentYearAmountText.color = new Color(255, 255, 255, objectColor.a);
                GameManager.Instance.UiManager.CurrentYearText.color = new Color(255, 255, 255, objectColor.a);

                fadeImage.color = objectColor;

                // increment the timer
                t += Time.deltaTime;

                // wait for the next frame
                yield return null;
            }

            // set the alpha value of the image to fully opaque
            currentSequenceState = sequenceState.SequenceCenter;
            fadeImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1f);
        }
        // if fading out
        else
        {
            // while the timer is less than the duration
            while (t < fadeDuration)
            {
                // set the alpha value of the image based on the timer
                Color objectColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(1f, 0f, t / fadeDuration));

                GameManager.Instance.UiManager.CurrentYearAmountText.color = new Color(255, 255, 255, objectColor.a);
                GameManager.Instance.UiManager.CurrentYearText.color = new Color(255, 255, 255, objectColor.a);

                fadeImage.color = objectColor;

                // increment the timer
                t += Time.deltaTime;

                // wait for the next frame
                yield return null;
            }

            // set the alpha value of the image to fully transparent
            fadeImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0f);
            currentSequenceState = sequenceState.notInSequence;
        }
    }

    public IEnumerator ChangeYearAmount(int targetYearAmount)
    {
        while (currentYear != targetYearAmount)
        {
            if (currentYear <= targetYearAmount)
            {
                currentYear++;
                GameManager.Instance.UiManager.CurrentYearAmountText.text = currentYear.ToString();
                yield return new WaitForSeconds(timeTravelSpeed);
            }
            else
            if (currentYear >= targetYearAmount)
            {
                currentYear--;
                GameManager.Instance.UiManager.CurrentYearAmountText.text = currentYear.ToString();
                yield return new WaitForSeconds(timeTravelSpeed);
            }
        }
        GameManager.Instance.EnvironmentManager.Progress = 0;
        if (targetYearAmount > 5000)
        {
            GameManager.Instance.EnvironmentManager.Progress = 1;
        }
        yield return new WaitForSeconds(1f);
        currentSequenceState = sequenceState.SequenceFadingOut;
        yield return null;
    }
}