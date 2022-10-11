using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates
{
    nothing = 0,
    onPlatform = 1,
    onTrain = 2
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    [SerializeField] private GameStates currentGameState = GameStates.nothing;
    [SerializeField] private TrainManager trainManager;

    public static GameManager Instance { get => instance; private set => instance = value; }
    public GameStates CurrentGameState { get => currentGameState; set => currentGameState = value; }

    // Start is called before the first frame update
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
