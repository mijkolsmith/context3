using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wagon : MonoBehaviour
{
    [SerializeField] private Transform leftWagonAttachPoint;
    [SerializeField] private Transform rightWagonAttachPoint;

    [SerializeField] private bool hasPlayerInside = false;

    public Transform LeftWagonAttachPoint { get => leftWagonAttachPoint; private set => leftWagonAttachPoint = value; }
    public Transform RightWagonAttachPoint { get => rightWagonAttachPoint; private set => rightWagonAttachPoint = value; }
    public bool HasPlayerInside { get => hasPlayerInside; private set => hasPlayerInside = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Enters train");
            HasPlayerInside = true;
            GameManager.Instance.CurrentGameState = GameStates.onTrain;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Exits train");
            HasPlayerInside = false;
            GameManager.Instance.CurrentGameState = GameStates.onPlatform;
        }
    }
}
