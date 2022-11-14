using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCompanion : MonoBehaviour
{

    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject playerCompanionParent;

    [SerializeField] private float speed = 2f; //The speed the robot moves at.

    [SerializeField] private float followDistance = 1f;

    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        
    }

    
    void LateUpdate()
    {
        FollowPlayer(playerCompanionParent.transform.position);
    }

    void FollowPlayer(Vector3 desiredPosition)
    {
        if (Vector3.Distance(transform.position,desiredPosition) > followDistance)
        {
            transform.position = Vector3.Lerp(transform.position, desiredPosition, speed * Time.deltaTime);
            transform.LookAt(player.gameObject.transform.position);
        }

    //    var step = speed * Time.deltaTime;
    //    transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, step);
    //
    }
}
