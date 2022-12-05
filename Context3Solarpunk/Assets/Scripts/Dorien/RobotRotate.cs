using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotRotate : MonoBehaviour
{
    [SerializeField] private GameObject playerCompanionParent;

    /// <summary>
    /// To fix choppiness, call the LookAtPlayer method in the LateUpdate method.
    /// </summary>
    void LateUpdate()
    {
        LookAtPlayer(playerCompanionParent.transform.position);
    }

    /// <summary>
    /// Make the robot look at the player.
    /// </summary>
    void LookAtPlayer(Vector3 playerPosition)
    {
        transform.LookAt(new Vector3(playerPosition.x,gameObject.transform.position.y,playerPosition.z));
    }
}