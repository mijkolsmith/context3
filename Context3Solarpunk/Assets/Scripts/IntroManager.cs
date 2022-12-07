using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private List<string> introDialogue = new List<string>();
    int currentDialogueIndex;

    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private GameObject previousButtonGameObject;
    [SerializeField] private GameObject nextButtonGameObject;

    [SerializeField] private Button sceneSwitchButton;

    private void Start()
    {
        dialogueText.text = introDialogue[0];
    }

    /// <summary>
    /// byAmount can be 1 to go ahead and -1 go go back
    /// </summary>
    /// <param name="byAmount"></param>
    public void ProgressDialogue(int byAmount)
    {
        if ((currentDialogueIndex + byAmount) > -1 && (currentDialogueIndex + byAmount) < introDialogue.Count)
        {
            currentDialogueIndex += byAmount;
        }

        if (currentDialogueIndex == 0)
        {
            previousButtonGameObject.SetActive(false);
        } 
        else
        {
            previousButtonGameObject.SetActive(true);
        }


        dialogueText.text = introDialogue[currentDialogueIndex];

        if (currentDialogueIndex == introDialogue.Count -1)
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
