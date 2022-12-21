using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    float firstClickTime = 0f;
    float timeBetweenClicks = 0.2f;
    int clickCounter = 0;

    //DoubleClick behaviour
    public bool DoubleClick()
    {
        if (Input.GetButtonDown("MoveKey"))
        {
            clickCounter++;
            if (clickCounter == 1) firstClickTime = Time.time;
        }
        if (Time.time - firstClickTime < timeBetweenClicks)
        {
            if (clickCounter > 1)
            {
                return true;
            }
        }
        else
        {
            clickCounter = 0;
            firstClickTime = 0;
        }
        return false;
    }
}
