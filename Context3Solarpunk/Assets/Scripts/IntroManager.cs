using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private string[] introDialogue;
    int currentDialogueIndex;

    [SerializeField] private Sprite[] introSprites;
    [SerializeField] private Image introImage;

    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private GameObject previousButtonGameObject;
    [SerializeField] private GameObject nextButtonGameObject;

    [SerializeField] private Button sceneSwitchButton;

    private void Start()
    {
        dialogueText.text = introDialogue[0];
        introImage.sprite = introSprites[0];
    }

    /// <summary>
    /// This method adjusts the dialogue visible on the screen, and also adjusts the buttons and background accordingly
    /// </summary>
    /// <param name="byAmount">1 to go ahead and -1 go back</param>
    public void ProgressDialogue(int byAmount)
    {
        if ((currentDialogueIndex + byAmount) > -1 && (currentDialogueIndex + byAmount) < introDialogue.Length)
        {
            currentDialogueIndex += byAmount;
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

        dialogueText.text = introDialogue[currentDialogueIndex];

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