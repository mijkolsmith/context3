using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{

    [SerializeField] private UnityEvent trainEvent;

    public UnityEvent TrainEvent { get => trainEvent; set => trainEvent = value; }

    private void Start()
    {
        TrainEvent = new UnityEvent();
    }
}
