using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//TODO: Maak dit het enige script dat iets te doen heeft met UI, maak 1 huidige UI instance die als enige tegelijk open kan staan.

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questText;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField, Range(0.001f, 0.2f)] private float textSpeed = 0.02f;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private GameObject canInteractPopupUIObject;
    [SerializeField] private GameObject canCraftPopupUIObject;
    [SerializeField] private GameObject dorienPopupUIObject;

    [SerializeField] private bool paused = false;

    public TextMeshProUGUI QuestText { get => questText; set => questText = value; }
    public GameObject CanInteractPopupUIObject { get => canInteractPopupUIObject; set => canInteractPopupUIObject = value; }
	public GameObject CanCraftPopupUIObject { get => canCraftPopupUIObject; set => canCraftPopupUIObject = value; }
	public GameObject DorienPopupUIObject { get => dorienPopupUIObject; set => dorienPopupUIObject = value; }

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
                    paused = true;
                    dialogueText.text = dialogueText.text.Substring(0, dialogueText.text.Length - 2);
                    yield return new WaitUntil(() => !paused/*Input.GetKeyDown(KeyCode.Return)*/);
                    dialogueText.text = "";
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
}