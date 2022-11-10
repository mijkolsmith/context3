using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject objectToFollow;

    [SerializeField] private float speed = 1f;

    //Camera movement behaviour always has to be in LateUpdate to fix choppyness
    //It fixes choppyness by being moved after physics movement calculations have been done
    private void LateUpdate()
    {
        CameraFollow(objectToFollow.transform.position);
    }

    void CameraFollow(Vector3 desiredPosition)
    {
        transform.position = Vector3.Lerp(transform.position, desiredPosition, speed * Time.deltaTime);
    }
}
