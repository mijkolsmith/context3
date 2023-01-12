using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraManager : MonoBehaviour
{
    [SerializeField] private GameObject cameraObject;

    [SerializeField] private List<Transform> cameraPositions = new List<Transform>();
    [SerializeField] private float cameraSpeed = 10f;

    [SerializeField] private Transform desiredPosition;
    


    private void Awake()
    {
        desiredPosition = cameraPositions[0];
    }

    //Lateupdate because of camera movements
    private void LateUpdate()
    {
        cameraObject.transform.position = Vector3.Lerp(cameraObject.transform.position, desiredPosition.position, cameraSpeed);
    }


    public void MoveCamera(int positionIndex)
    {
        desiredPosition = cameraPositions[positionIndex];
    }
}
