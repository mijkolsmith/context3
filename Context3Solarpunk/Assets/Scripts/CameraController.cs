using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject objectToFollow;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] public float cameraDistance = 11.7f;
    [SerializeField] public float cameraHeight = 3f;
    private new Camera camera;

    /// <summary>
    /// Get the main camera object.
    /// </summary>
    private void Start()
    {
        camera = Camera.main;
    }

    /// <summary>
    /// To fix choppyness, Camera movement behaviour has to be in the LateUpdate function. 
    /// It does this by being moved after physics movement calculations have been done.
    /// </summary>
    private void LateUpdate()
    {
        CameraFollow(objectToFollow.transform.position);
    }

    /// <summary>
    /// Make the camera follow the correct object using the eventual new height and distance modifiers.
    /// </summary>
    /// <param name="desiredPosition"></param>
    private void CameraFollow(Vector3 desiredPosition)
    {
        transform.position = Vector3.Lerp(transform.position, desiredPosition, moveSpeed * Time.deltaTime);
        camera.transform.localPosition = new Vector3(camera.transform.localPosition.x,
            Mathf.Lerp(camera.transform.localPosition.y, cameraHeight, zoomSpeed * Time.deltaTime),
            Mathf.Lerp(camera.transform.localPosition.z, -cameraDistance, zoomSpeed * Time.deltaTime)
            );
    }
}
