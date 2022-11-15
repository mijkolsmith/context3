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

	private void Start()
	{
        camera = Camera.main;
    }

	//Camera movement behaviour always has to be in LateUpdate to fix choppyness
	//It fixes choppyness by being moved after physics movement calculations have been done
	private void LateUpdate()
    {
        CameraFollow(objectToFollow.transform.position);
    }

    private void CameraFollow(Vector3 desiredPosition)
    {
        transform.position = Vector3.Lerp(transform.position, desiredPosition, moveSpeed * Time.deltaTime);
        camera.transform.localPosition = new Vector3(camera.transform.localPosition.x, Mathf.Lerp(camera.transform.localPosition.y, cameraHeight, zoomSpeed * Time.deltaTime), Mathf.Lerp(camera.transform.localPosition.z, -cameraDistance, zoomSpeed * Time.deltaTime));
    }
}
