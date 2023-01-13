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
                TimeTravelToTheFuture();
            }
            else
            {
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
        targetYear = 2082;
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
        targetYear = 2022;
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
            fadeImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1f);
            currentSequenceState = sequenceState.SequenceCenter;
            Sequence(environmentNumber);
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

        if (startDelayedDialogue)
        {
            GameManager.Instance.UiManager.StartDialogue(savedDelayedDialogue);
            startDelayedDialogue = false;
        }
        yield return null;
    }

    public void TimeTravelSetDelayedDialogue(string dialogue)
	{
        savedDelayedDialogue = dialogue;
        startDelayedDialogue = true;
    }
}