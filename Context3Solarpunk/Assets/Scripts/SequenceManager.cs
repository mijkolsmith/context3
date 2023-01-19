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

    [SerializeField] private int currentYear = 2083;

    [SerializeField] private sequenceState currentSequenceState = sequenceState.notInSequence;
    [SerializeField] private GameObject blackoutSquareUI;
    [SerializeField] private float transitionSpeed = 0.0025f;
    [SerializeField] private float timeTravelSpeed = 15f;

    [SerializeField] private float countDuration = 2.5f;
    [SerializeField] private float fadeDuration = 1.5f;

    //delayed dialogue
    private string savedDelayedDialogue;
    private bool startDelayedDialogue;

    int environmentNumber = 0;

    private bool advanced = false;

    private float fadeSpeed = 5f;

    [SerializeField, ReadOnly] private int targetYear;

    private float t; //a timer

    /// <summary>
    /// Preset method that can be executed in the inspector to go to any year with param
    /// </summary>
    /// <param name="year"></param>
    public void TimeTravel()
    {
        //If you want to go to the newest future, complete all tasks in current past timeline and trigger CompletedAllTasksInTimeline().
        if (advanced)
        {
            environmentNumber++;
        }
        else environmentNumber = GameManager.Instance.EnvironmentManager.InThePast ? environmentNumber - 1 : environmentNumber + 1;
        advanced = false;

        if (!timeTravelling)
        {
            timeTravelling = true;
            currentSequenceState = sequenceState.sequenceFadingIn;
            
            if (GameManager.Instance.EnvironmentManager.InThePast)
            {
                GameManager.Instance.SoundManager.PlayOneShotSound(SoundName.TIME_TRAVEL_TO_FUTURE);
                TimeTravelToTheFuture();
            }
            else
            {
                GameManager.Instance.SoundManager.PlayOneShotSound(SoundName.TIME_TRAVEL_TO_PAST);
                TimeTravelToThePast();
            }
        }
        Sequence(environmentNumber);
    }

    /// <summary>
    /// Preset method that can be executed in the inspector to go to 2082 with sequence
    /// </summary>
    [ContextMenu("Run travel sequence to the future")]
    public void TimeTravelToTheFuture()
    {
        targetYear = 2083;
        GameManager.Instance.EnvironmentManager.InThePast = false;
    }

    public void CompletedAllTasksInTimeline()
    {
        advanced = true;
    }

    /// <summary>
    /// Preset method that can be executed in the inspector to go to 2022 with sequence
    /// </summary>
    [ContextMenu("Run travel sequence to the past")]
    public void TimeTravelToThePast()
    {
        targetYear = 2023;
        GameManager.Instance.EnvironmentManager.InThePast = true;
    }

    private void Sequence(int environmentNumber)
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
                    GameManager.Instance.EnvironmentManager.Progress = environmentNumber;
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

        // Get the current color of the image
        Color currentColor = fadeImage.color;

        // Create a timer
        float t = 0f;

        // If fading in
        if (fadeIn)
        {
            GameManager.Instance.UiManager.TogglePopupWindow(PopupWindowType.BlackoutPanel);

            // While the timer is less than the duration
            while (t < fadeDuration)
            {
                // Set the alpha value of the image based on the timer
                Color objectColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(0f, 1f, t / fadeDuration));

                GameManager.Instance.UiManager.CurrentYearAmountText.color = new Color(255, 255, 255, objectColor.a);
                GameManager.Instance.UiManager.CurrentYearText.color = new Color(255, 255, 255, objectColor.a);

                fadeImage.color = objectColor;

                // Increment the timer
                t += Time.deltaTime;

                // Wait for the next frame
                yield return null;
            }

            // Set the alpha value of the image & text to fully opaque
            fadeImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1f);
            GameManager.Instance.UiManager.CurrentYearAmountText.color = new Color(255, 255, 255, 1f);
            GameManager.Instance.UiManager.CurrentYearText.color = new Color(255, 255, 255, 1f);
            
            // Toggle the time indicator year count
            GameManager.Instance.UiManager.ToggleTimeIndicator();
            
            currentSequenceState = sequenceState.SequenceCenter;
            Sequence(environmentNumber);
        }
        // If fading out
        else
        {
            // While the timer is less than the duration
            while (t < fadeDuration)
            {
                // Set the alpha value of the image based on the timer
                Color objectColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(1f, 0f, t / fadeDuration));

                GameManager.Instance.UiManager.CurrentYearAmountText.color = new Color(255, 255, 255, objectColor.a);
                GameManager.Instance.UiManager.CurrentYearText.color = new Color(255, 255, 255, objectColor.a);

                fadeImage.color = objectColor;

                // Increment the timer
                t += Time.deltaTime;

                // Wait for the next frame
                yield return null;
            }

            // Set the alpha value of the image & text to fully transparent
            fadeImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0f);
            GameManager.Instance.UiManager.CurrentYearAmountText.color = new Color(255, 255, 255, 0);
            GameManager.Instance.UiManager.CurrentYearText.color = new Color(255, 255, 255, 0);

            GameManager.Instance.UiManager.TogglePopupWindow(PopupWindowType.BlackoutPanel);

            if (startDelayedDialogue)
            {
                GameManager.Instance.UiManager.StartDorienDialogue(savedDelayedDialogue);
                startDelayedDialogue = false;
            }

            currentSequenceState = sequenceState.notInSequence;
            Sequence(environmentNumber);
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
                yield return new WaitForSeconds(0.025f);
            }
            else
            if (currentYear >= targetYearAmount)
            {
                currentYear--;
                GameManager.Instance.UiManager.CurrentYearAmountText.text = currentYear.ToString();
                yield return new WaitForSeconds(0.025f);
            }
        }
        yield return new WaitForSeconds(1f);

        currentSequenceState = sequenceState.SequenceFadingOut;
        Sequence(environmentNumber);

        yield return null;
    }

    public void TimeTravelSetDelayedDialogue(string dialogue)
	{
        savedDelayedDialogue = dialogue;
        startDelayedDialogue = true;
    }
}