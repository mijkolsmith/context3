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

    [SerializeField] private int currentYear = 2022;

    [SerializeField] private sequenceState currentSequenceState = sequenceState.notInSequence;
    [SerializeField] private GameObject blackoutSquareUI;
    [SerializeField] private float transitionSpeed = 0.0025f;
    [SerializeField] private float timeTravelSpeed = 15f;

    private float fadeSpeed = 5f;

    [SerializeField, ReadOnly] private int targetYear;

    /// <summary>
    /// Preset method that can be executed in the inspector to go to any year with param
    /// </summary>
    /// <param name="year"></param>
    public void TimeTravel(int year)
    {
        targetYear = year;
        timeTravelling = true;
        currentSequenceState = sequenceState.sequenceFadingIn;
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
                    StartCoroutine(ChangeScreenFade(1));
                    break;
                case sequenceState.SequenceCenter:
                    Debug.Log(targetYear);
                    StartCoroutine(ChangeYearAmount(targetYear));
                    break;
                case sequenceState.SequenceFadingOut:
                    StartCoroutine(ChangeScreenFade(0));
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// Change year text in the sequence gradually
    /// </summary>
    /// <param name="targetYearAmount"></param>
    /// <returns></returns>
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

    /// <summary>
    /// This fades the screen with the amount given by param, minus makes screen invisible and positive hides it
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    public IEnumerator ChangeScreenFade(int amount)
    {
        Color objectColor = blackoutSquareUI.GetComponent<Image>().color;

        while (objectColor.a != amount)
        {
            if (objectColor.a <= amount)
            {
                objectColor.a += transitionSpeed;
                GameManager.Instance.UiManager.CurrentYearText.color = new Color(255, 255, 255, amount);
                GameManager.Instance.UiManager.CurrentYearAmountText.color = new Color(255, 255, 255, amount);
                blackoutSquareUI.GetComponent<Image>().color = objectColor;
                if (Mathf.Abs(objectColor.a - amount) < 0.01f)
                {
                    objectColor.a = amount;
                }
                yield return new WaitForSeconds(GameManager.Instance.UiManager.TextSpeed * Time.deltaTime);
            }
            else
            if (objectColor.a >= amount)
            {
                objectColor.a -= transitionSpeed;
                GameManager.Instance.UiManager.CurrentYearText.color = new Color(255, 255, 255, amount);
                GameManager.Instance.UiManager.CurrentYearAmountText.color = new Color(255, 255, 255, amount);
                blackoutSquareUI.GetComponent<Image>().color = objectColor;
                if (Mathf.Abs(objectColor.a - amount) < 0.01f)
                {
                    objectColor.a = amount;
                }
                yield return new WaitForSeconds(GameManager.Instance.UiManager.TextSpeed * Time.deltaTime);

            }
        }
        if (objectColor.a == 0)
        {
            currentSequenceState = sequenceState.notInSequence;
        }
        if (objectColor.a == 1)
        {
            currentSequenceState = sequenceState.SequenceCenter;
        }
        yield return null;
    }
}
