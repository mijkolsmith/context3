using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBusAnimation : MonoBehaviour
{
    [SerializeField] private GameObject busObject;
    [SerializeField] private Transform desiredPositionTransform;
    [SerializeField] private float speed;

    private void Update()
    {
        busObject.transform.position = Vector3.Lerp(busObject.transform.position, desiredPositionTransform.position, speed);
    }
}
