using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotFollow : MonoBehaviour
{
    [SerializeField] private GameObject playerCompanionParent;
    [SerializeField] private float speed = 2f; //The speed the robot moves at.
    [SerializeField] private float followDistance = 1f;

    void LateUpdate()
    {
        FollowPlayer(playerCompanionParent.transform.position);
    }

    void FollowPlayer(Vector3 desiredPosition)
    {
        if (Vector3.Distance(transform.position, desiredPosition) > followDistance)
        {
            transform.position = Vector3.Lerp(transform.position, desiredPosition, speed * Time.deltaTime);
        }
    }
}