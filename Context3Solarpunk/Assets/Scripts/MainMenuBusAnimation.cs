using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBusAnimation : MonoBehaviour
{

    [SerializeField] private AnimationCurve MoveCurve;
    [SerializeField] private GameObject busObject;


    [SerializeField ]private Transform _target;
    private Vector3 _startPoint;
    private float _animationTimePosition;

    private void Awake()
    {
        _startPoint = busObject.transform.position;
    }

    private void Update()
    {
        if (_target.position != busObject.transform.position)
        {
            _animationTimePosition += Time.deltaTime;
            busObject.transform.position = Vector3.Lerp(_startPoint, _target.position, MoveCurve.Evaluate(_animationTimePosition));
        }
    }
}



    //[SerializeField] private GameObject busObject;
    //[SerializeField] private Transform desiredPositionTransform;
    //[SerializeField] private float speed;



    //private void FixedUpdate()
    //{
    //    busObject.transform.position = Vector3.Lerp(busObject.transform.position, desiredPositionTransform.position, speed);
    //}

