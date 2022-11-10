using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject objectToFollow;

    [SerializeField] private float speed = 1f;

    private void LateUpdate()
    {
        CameraFollow(objectToFollow.transform.position);
    }

    void CameraFollow(Vector3 desiredPosition)
    {
        transform.position = Vector3.Lerp(transform.position, desiredPosition, speed * Time.deltaTime);
    }
}
