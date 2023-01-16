using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private string[] introDialogue;
    int currentDialogueIndex = 0;

    [SerializeField] private Sprite[] introSprites;
    [SerializeField] private Image introImage;

    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField, Range(0.0001f, 0.05f)] private float textSpeed = 0.02f;

    [SerializeField] private GameObject previousButtonGameObject;
    [SerializeField] private GameObject nextButtonGameObject;

    [SerializeField] private Button sceneSwitchButton;

    private void Start()
    {
        introImage.sprite = introSprites[0];
        StartDialogue();
    }

    public void NextSentence()
	{
        if (currentDialogueIndex + 1 < introDialogue.Length) currentDialogueIndex++;
        StartDialogue();
    }

    public void PreviousSentence()
    {
        if(currentDialogueIndex - 1 > -1) currentDialogueIndex--;
        StartDialogue();
    }

    public void StartDialogue()
	{
        StopAllCoroutines();
        StartCoroutine(TypeSentence(introDialogue[currentDialogueIndex]));
    }

    public IEnumerator TypeSentence(string sentence)
    {
        nextButtonGameObject.SetActive(false);

        //Set the right text size
        dialogueText.enableAutoSizing = true;
        dialogueText.text = sentence;
        dialogueText.ForceMeshUpdate();
        float autoFontSize = dialogueText.fontSize;
        dialogueText.text = "";
        dialogueText.enableAutoSizing = false;
        dialogueText.fontSize = autoFontSize;

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        if (currentDialogueIndex == 0)
        {
            previousButtonGameObject.SetActive(false);
            introImage.sprite = introSprites[0];
        }
        else
        {
            previousButtonGameObject.SetActive(true);
            if (currentDialogueIndex == 1)
            {
                introImage.sprite = introSprites[1];
            }
            else if (currentDialogueIndex == 3)
            {
                introImage.sprite = introSprites[2];
            }
            else introImage.sprite = introSprites[0];
        }

        if (currentDialogueIndex == introDialogue.Length - 1)
        {
            sceneSwitchButton.gameObject.SetActive(true);
            nextButtonGameObject.SetActive(false);
        }
        else
        {
            sceneSwitchButton.gameObject.SetActive(false);
            nextButtonGameObject.SetActive(true);
        }
    }
}