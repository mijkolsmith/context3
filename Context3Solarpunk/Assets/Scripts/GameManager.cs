using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance { get => instance; private set => instance = value; }

    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private QuestManager questManager;
    [SerializeField] private TrainManager trainManager;
    [SerializeField] private EnvironmentManager environmentManager;
    [SerializeField] private CraftingManager craftingManager;
    private GameStateManager gameStateManager;
    public TrainManager TrainManager { get => trainManager; private set => trainManager = value; }
    public EnvironmentManager EnvironmentManager { get => environmentManager; private set => environmentManager = value; }
    public GameStateManager GameStateManager { get => gameStateManager; private set => gameStateManager = value; }
    public CraftingManager CraftingManager { get => craftingManager; private set => craftingManager = value; }
    public QuestManager QuestManager { get => questManager; private set => questManager = value; }
    public DialogueManager DialogueManager { get => dialogueManager; private set => dialogueManager = value; }
	public PopupWindow[] PopupWindows { get => popupWindows; private set => popupWindows = value; }

	[SerializeField] private Slider passengerHappinessSlider;
    [MinValue(0), MaxValue(3), ReadOnly] public int trashCount;
    [SerializeField] private Sprite[] cupSprites;
    [SerializeField] private Image[] trashHolders;
    [SerializeField] private Image[] indicators;
    [SerializeField] private PopupWindow[] popupWindows;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        } 
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        GameStateManager = gameObject.AddComponent<GameStateManager>();
    }

    public void ChangePassengerHappiness(int amount)
	{
        passengerHappinessSlider.value += amount;
	}

    public int GetPassengerHappiness()
    {
        return (int)passengerHappinessSlider.value;
    }

    public void AddTrashToInventory(TrashType trashType)
	{
        foreach (Image trashHolder in trashHolders)
		{
            if (trashHolder.sprite == cupSprites[0])
			{
                trashHolder.sprite = cupSprites[(int)trashType];
                break;
            }
		}
	}

    public bool RemoveTrashFromInventory(TrashType trashType)
    {
        foreach (Image trashHolder in trashHolders)
        {
            if (trashHolder.sprite == cupSprites[1] && trashType == TrashType.TRASH1 || 
                trashHolder.sprite == cupSprites[2] && trashType == TrashType.TRASH2 || 
                trashHolder.sprite == cupSprites[3] && trashType == TrashType.TRASH3)
            {
                trashHolder.sprite = cupSprites[0];
                return true;
            }
        }
        return false;
    }

    public void ToggleIndicator(int indicator)
	{
        indicators[indicator].enabled = !indicators[indicator].enabled;
    }

    public void TogglePopupWindow(PopupWindowType popupWindowType)
    {
        PopupWindows.Where(x => x.GetPopupWindowType() == popupWindowType).FirstOrDefault().Open();
    }
}