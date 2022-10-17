using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    //TODO: Couple with a UIManager 
    [SerializeField]
    private GameObject dialoguePanel;

    [SerializeField, Range(0.001f,0.2f)] private float textSpeed = 0.02f;

    float timer = 0;
    [SerializeField]
    private TextMeshProUGUI dialogueText;


    private void Start()
    {
        StartDialogue("Dit is een heel cool startverhaaltje die niet erg logisch is maar wel een verhaaltje is.");
    }

    public void StartDialogue(string dialogue)
    {
        dialoguePanel.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(TypeSentence(dialogue));
    }

    public IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            if (dialogueText.text.Length > 2)
            {
                string s = dialogueText.text.Substring(dialogueText.text.Length - 2);
                if (s == ".n" || s == "!n")
                {
                    dialogueText.text = "";
                }
            }
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public IEnumerator ShowDialogueScreen(float seconds)
    {

        dialoguePanel.SetActive(false);
        yield return new WaitForSeconds(seconds);
    }
}