using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//TODO: Cleanup / Remove all unnecessary code
//TODO: Add Summaries

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance { get => instance; private set => instance = value; }

    [SerializeField] private UIManager uiManager;
    [SerializeField] private QuestManager questManager;
    [SerializeField] private TrainManager trainManager;
    [SerializeField] private EnvironmentManager environmentManager;
    [SerializeField] private CraftingManager craftingManager;
    [SerializeField] private GameStateManager gameStateManager;
    public TrainManager TrainManager { get => trainManager; private set => trainManager = value; }
    public EnvironmentManager EnvironmentManager { get => environmentManager; private set => environmentManager = value; }
    public GameStateManager GameStateManager { get => gameStateManager; private set => gameStateManager = value; }
    public CraftingManager CraftingManager { get => craftingManager; private set => craftingManager = value; }
    public QuestManager QuestManager { get => questManager; private set => questManager = value; }
    public UIManager DialogueManager { get => uiManager; private set => uiManager = value; }
	public PopupWindow[] PopupWindows { get => popupWindows; private set => popupWindows = value; }
    public UIManager UiManager { get => uiManager; set => uiManager = value; }

    [MinValue(0), MaxValue(3), ReadOnly] public int trashCount;
    [SerializeField] private PopupWindow[] popupWindows;

    /// <summary>
    /// Singleton pattern and assign managers.
    /// </summary>
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

    /// <summary>
    /// Toggle a certain popupWindow.
    /// </summary>
    /// <param name="popupWindowType"></param>
    public void TogglePopupWindow(PopupWindowType popupWindowType)
    {
        //TODO: move to UIManager
        //TODO: only one should be able to open at a time.
        PopupWindows.Where(x => x.GetPopupWindowType() == popupWindowType).FirstOrDefault().Toggle();
    }
}