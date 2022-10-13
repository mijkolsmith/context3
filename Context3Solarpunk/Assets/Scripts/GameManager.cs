using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance { get => instance; private set => instance = value; }

    [SerializeField] private TrainManager trainManager;
    private GameStateManager gameStateManager;
    public TrainManager TrainManager { get => trainManager; private set => trainManager = value; }
    public GameStateManager GameStateManager { get => gameStateManager; private set => gameStateManager = value; }

    [SerializeField] private Slider passengerHappinessSlider;
    [MinValue(0), MaxValue(3)] public int trashCount;

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

    void Update()
    {
        
    }

    public void ChangePassengerHappiness(int amount)
	{
        passengerHappinessSlider.value += amount;
	}
}
