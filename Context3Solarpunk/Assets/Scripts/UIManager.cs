using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questText;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField, Range(0.001f, 0.2f)] private float textSpeed = 0.02f;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private GameObject canInteractPopupUIObject;

    [SerializeField] private bool paused = false;


    float timer = 0;

    public TextMeshProUGUI QuestText { get => questText; set => questText = value; }
    public GameObject CanInteractPopupUIObject { get => canInteractPopupUIObject; set => canInteractPopupUIObject = value; }

    private void Start()
    {
        StartDialogue("");
    }

    public void StartDialogue(string dialogue)
    {
        //dialoguePanel.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(TypeSentence(dialogue));
    }

    [ContextMenu("ContinueDialogueDebug")]
    public void ContinueDialogue()
    {
        paused = false;
    }

    public IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            if (dialogueText.text.Length > 1)
            {
                string s = dialogueText.text.Substring(dialogueText.text.Length - 1);
                Debug.Log(s);
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

    public void ShowDialogueScreen(bool show)
    {
        dialoguePanel.SetActive(show);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ContinueDialogue();
        }
    }
}