using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using NaughtyAttributes;
using System.Linq;

//TODO: Maak dit het enige script dat iets te doen heeft met UI, maak 1 huidige UI instance die als enige tegelijk open kan staan.

public class UIManager : MonoBehaviour
{
#region Variables
    [SerializeField] private TextMeshProUGUI questText;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField, Range(0.0001f, 0.05f)] private float textSpeed = 0.02f;
    [SerializeField] private TextMeshProUGUI[] dialogueTexts;
    [SerializeField] private TextMeshProUGUI activeDialogueText;

    [SerializeField] private GameObject canInteractPopupUIObject;
    [SerializeField] private GameObject canCraftPopupUIObject;
    [SerializeField] private GameObject dorienPopupUIObject;

    [SerializeField] private bool paused = false;

    [SerializeField] private GameObject blackoutSquare;
    [SerializeField] private TextMeshProUGUI currentYearText;
    [SerializeField] private TextMeshProUGUI currentYearAmountText;

    // Popup Windows
    [SerializeField] private PopupWindow[] popupWindows;
    [ReadOnly] public PopupWindowType popupWindowOpenType = PopupWindowType.None;

    // Text Size
    private Dictionary<TextMeshProUGUI, float> fontSizes = new();
    private float fontSizeModifier = 1f;

    private bool screenfaded = false;
    public bool blocking = true;
#endregion

#region Properties
	public TextMeshProUGUI QuestText { get => questText; set => questText = value; }
    public GameObject CanInteractPopupUIObject { get => canInteractPopupUIObject; set => canInteractPopupUIObject = value; }
    public GameObject CanCraftPopupUIObject { get => canCraftPopupUIObject; set => canCraftPopupUIObject = value; }
    public GameObject DorienPopupUIObject { get => dorienPopupUIObject; set => dorienPopupUIObject = value; }
    public TextMeshProUGUI CurrentYearText { get => currentYearText; set => currentYearText = value; }
    public TextMeshProUGUI CurrentYearAmountText { get => currentYearAmountText; set => currentYearAmountText = value; }
    public float TextSpeed { get => textSpeed; set => textSpeed = value; }
    public PopupWindow[] PopupWindows { get => popupWindows; private set => popupWindows = value; }
	public float FontSizeModifier { get => fontSizeModifier; set
        {
            fontSizeModifier = value;
            UpdateFontSize();
        }
    }
#endregion
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ContinueDialogue();
        }
    }

    /// <summary>
    /// Starts dialogue in dialogueText object.
    /// </summary>
    /// <param name="dialogue"></param>
    public void StartDialogue(string dialogue)
    {
        StopAllCoroutines();
        CheckBlockingToggle();
        StartCoroutine(TypeSentence(dialogue));
    }

    /// <summary>
    /// Sets "paused" bool to false so 
    /// </summary>
    [ContextMenu("ContinueDialogueDebug")]
    public void ContinueDialogue()
    {
        paused = false;
    }

    private void CheckBlockingToggle()
	{
        if (!blocking)
        {
            dialoguePanel.SetActive(!dialoguePanel.activeInHierarchy);
            activeDialogueText = dialogueTexts[0];
        }
        else
        {
            TogglePopupWindow(PopupWindowType.Dialogue);
            activeDialogueText = dialogueTexts[1];
        }
    }

    /// <summary>
    /// Enumerator that actually types the sentence and pauses when "paused" bool is set to true.
    /// </summary>
    /// <param name="sentence"></param>
    /// <returns></returns>
    public IEnumerator TypeSentence(string sentence)
    {
        GameManager.Instance.SoundManager.StopSound();

        List<string> sentences = new();
        activeDialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            activeDialogueText.text += letter;
            if (letter == '|' || letter == '*')
            {
                sentences.Add(activeDialogueText.text.Substring(0, activeDialogueText.text.Length - 1));
                activeDialogueText.text = "";
            }
        }

        foreach (string splitSentence in sentences)
        {
            //Set the right text size
            activeDialogueText.enableAutoSizing = true;
            activeDialogueText.text = splitSentence;
            activeDialogueText.ForceMeshUpdate();
            float autoFontSize = activeDialogueText.fontSize;
            activeDialogueText.text = "";
            activeDialogueText.enableAutoSizing = false;
            activeDialogueText.fontSize = autoFontSize;

            GameManager.Instance.SoundManager.PlaySound(SoundName.DORIEN_TALKING);
            foreach (char letter in splitSentence)
            {
                activeDialogueText.text += letter;
                yield return new WaitForSeconds(textSpeed);
            }

            GameManager.Instance.SoundManager.PauseSound();
            paused = true;
            yield return new WaitUntil(() => !paused);
            activeDialogueText.text = "";
        }
        CheckBlockingToggle();
        GameManager.Instance.SoundManager.StopSound();
    }

    /// <summary>
    /// Toggle a certain popupWindow.
    /// </summary>
    /// <param name="popupWindowType"></param>
    public void TogglePopupWindow(PopupWindowType popupWindowType)
    {
        if (popupWindowType == popupWindowOpenType || popupWindowOpenType == PopupWindowType.None)
        {
            PopupWindows.Where(x => x.GetPopupWindowType() == popupWindowType).FirstOrDefault().Toggle();
            UpdateFontSize();
        }
    }

    /// <summary>
    /// Toggle a certain popupWindow with a button.
    /// </summary>
    /// <param name="popupWindowComponent"></param>
    public void TogglePopupWindow(PopupWindowComponent popupWindowComponent)
    {
        if (popupWindowComponent.popupWindow == popupWindowOpenType || popupWindowOpenType == PopupWindowType.None)
        {
            PopupWindows.Where(x => x.GetPopupWindowType() == popupWindowComponent.popupWindow).FirstOrDefault().Toggle();
            UpdateFontSize();
        }
    }

	/// <summary>
	/// Update all the fontSizes in the game
	/// </summary>
	private void UpdateFontSize()
	{
        List<TextMeshProUGUI> textComponents = FindObjectsOfType<TextMeshProUGUI>(false).ToList();

        foreach (TextMeshProUGUI textComponent in textComponents)
        {
            if (textComponent != null)
            {
                if (!fontSizes.ContainsKey(textComponent))
                {
                    fontSizes.Add(textComponent, textComponent.fontSizeMax);
                }
                textComponent.fontSizeMax = fontSizes[textComponent] * FontSizeModifier;
            }
        }
    }

    public void SetQuestText(string text)
	{
        questText.text = text;
	}
}