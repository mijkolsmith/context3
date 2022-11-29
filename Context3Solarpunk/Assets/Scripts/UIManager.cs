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
    [SerializeField, Range(0.001f, 0.2f)] private float textSpeed = 0.02f;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private GameObject canInteractPopupUIObject;
    [SerializeField] private GameObject canCraftPopupUIObject;
    [SerializeField] private GameObject dorienPopupUIObject;

    [SerializeField] private bool paused = false;
    [SerializeField, ReadOnly] private bool dialogueFinished = false;

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

	private void Start()
    {
        StartDialogue("");
    }

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
        //dialoguePanel.SetActive(true);

        StopAllCoroutines();
        dialoguePanel.SetActive(true);
        StartCoroutine(TypeSentence(dialogue));
    }



    /// <summary>
    /// Sets "paused" bool to false so 
    /// </summary>
    [ContextMenu("ContinueDialogueDebug")]
    public void ContinueDialogue()
    {
        paused = false;
        if (dialogueFinished)
        {
            dialoguePanel.SetActive(false);
            dialogueFinished = false;
        }
        
    }

    /// <summary>
    /// Enumerator that actually types the sentence and pauses when "paused" bool is set to true.
    /// </summary>
    /// <param name="sentence"></param>
    /// <returns></returns>
    public IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            if (dialogueText.text.Length > 1)
            {
                string s = dialogueText.text.Substring(dialogueText.text.Length - 1);
                if (s == "|")
                {
                    paused = true; //Pause the dialogue
                    dialogueText.text = dialogueText.text.Substring(0, dialogueText.text.Length - 1); //Takes the last character away from the text (presumably a "|")
                    yield return new WaitUntil(() => !paused/*Input.GetKeyDown(KeyCode.Return)*/);
                    dialogueFinished = false;
                    dialogueText.text = "";
                } 
                else if (s == "*")
                {
                    dialogueText.text = dialogueText.text.Substring(0, dialogueText.text.Length - 1);
                    dialogueFinished = true;
                }
            }
            yield return new WaitForSeconds(textSpeed);
        }
    }

    /// <summary>
    /// Sets the dialogue screen gameobject to active or not depending on param
    /// </summary>
    /// <param name="show"></param>
    public void ShowDialogueScreen(bool show)
    {
        dialoguePanel.SetActive(show);
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
}