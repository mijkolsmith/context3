using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance { get => instance; private set => instance = value; }

    [SerializeField] private UIManager uiManager;
    [SerializeField] private QuestManager questManager;
    [SerializeField] private EnvironmentManager environmentManager;
    [SerializeField] private CraftingManager craftingManager;
    [SerializeField] private GameStateManager gameStateManager;
    [SerializeField] private SequenceManager sequenceManager;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private InputManager inputManager;

    [SerializeField] private NavMeshSurface surface;
    public EnvironmentManager EnvironmentManager { get => environmentManager; private set => environmentManager = value; }
    public GameStateManager GameStateManager { get => gameStateManager; private set => gameStateManager = value; }
    public CraftingManager CraftingManager { get => craftingManager; private set => craftingManager = value; }
    public QuestManager QuestManager { get => questManager; private set => questManager = value; }
    public UIManager UiManager { get => uiManager; set => uiManager = value; }
    public SequenceManager SequenceManager { get => sequenceManager; set => sequenceManager = value; }
	public SoundManager SoundManager { get => soundManager; set => soundManager = value; }
    public InputManager InputManager { get => inputManager; set => inputManager = value; }

    [MinValue(0), MaxValue(3), ReadOnly] public int trashCount;

    /// <summary>
    /// Singleton pattern and assign managers.
    /// </summary>
    private void Awake()
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
        SequenceManager = GetComponent<SequenceManager>();
        EnvironmentManager = GetComponent<EnvironmentManager>();
		SoundManager = GetComponent<SoundManager>();
		InputManager = GetComponent<InputManager>();
    }

	/// <summary>
	/// Serializes & Saves position, quest progress and inventory
	/// </summary>
	public void SaveAndQuit()
	{
        //TODO: Save quest progress
        //TODO: Save position
        CraftingManager.SaveInventory();

        //TODO: Loading

        //SceneManager.LoadScene(0);
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    /// <summary>
    /// Refreshes the NavMesh after changing the environment during playmode.
    /// </summary>
    public void RefreshNavMesh()
	{
        surface.BuildNavMesh();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            Debug.Log("Scene reset");
            SceneManager.LoadScene("IntroScene");
        }
    }
}