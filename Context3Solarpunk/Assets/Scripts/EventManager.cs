using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    [SerializeField] private UnityEvent trainEventLeft;
    [SerializeField] private UnityEvent trainEventHome;
    [SerializeField] private UnityEvent trainEventRight;

    public UnityEvent TrainEventLeft { get => trainEventLeft; set => trainEventLeft = value; }
    public UnityEvent TrainEventHome { get => trainEventHome; set => trainEventHome = value; }
    public UnityEvent TrainEventRight { get => trainEventRight; set => trainEventRight = value; }

    //private void Start()
    //{
    //    trainEventLeft = new UnityEvent();
    //    trainEventHome = new UnityEvent();
    //    trainEventRight = new UnityEvent();
    //}
}
