using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance { get => instance; private set => instance = value; }

    [SerializeField] private TrainManager trainManager;
    [SerializeField] private GameStateManager gameStateManager;
    public TrainManager TrainManager { get => trainManager; private set => trainManager = value; }
    public GameStateManager GameStateManager { get => gameStateManager; private set => gameStateManager = value; }

    [SerializeField] private Slider passengerHappinessSlider;

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
