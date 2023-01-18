using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DorienManager : MonoBehaviour
{
    [SerializeField] private RobotFollow follow;
    [SerializeField] private RobotRotate rotate;

    public void SetRobot(Transform target)
    {
        follow.PlayerCompanionParent = target.gameObject;
        rotate.PlayerCompanionParent = target.gameObject;
    }
}
