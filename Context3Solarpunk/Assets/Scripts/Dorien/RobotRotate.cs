using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: add summaries / comments
public class RobotRotate : MonoBehaviour
{
    [SerializeField] private GameObject playerCompanionParent;

    void LateUpdate()
    {
        FollowPlayer(playerCompanionParent.transform.position);
    }

    void FollowPlayer(Vector3 desiredPosition)
    {
        transform.LookAt(desiredPosition);
    }
}