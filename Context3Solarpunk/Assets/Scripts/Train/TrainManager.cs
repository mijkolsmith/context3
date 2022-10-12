using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    [SerializeField] private Transform stationLocation; //Could be done with Vector3 but transforms are easier
    [SerializeField] private Vector3 desiredPosition;
    [SerializeField] private float speed = 1.0f;

    private Vector3 velocity = Vector3.zero;

    public Train train;

	private void Start()
	{
        InstantiateWagons();
	}

    private void InstantiateWagons()
    {
        desiredPosition = stationLocation.position;

        for (int i = 0; i < train.wagons.Count; i++)
        {
            if (i == 0) 
            {
                GameObject go = Instantiate(train.wagons[i].wagonObject, stationLocation);
            } 
            else
            {
                GameObject go = Instantiate(train.wagons[i].wagonObject, new Vector3(
                    stationLocation.position.x + (i * 16), 
                    stationLocation.position.y, 
                    stationLocation.position.z), 
                    Quaternion.identity, stationLocation);
            }
        }
    }

    private void Update()
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, step);
        //only move in OnMovingTrainState
    }

    public void SetDesiredLocationLeft()
    {
        desiredPosition = new Vector3(-100,0,5);
        Debug.Log("left");
    }

    public void SetDesiredLocationRight()
    {
        desiredPosition = new Vector3(100,0,5);
        Debug.Log("right");
    }

    public void SetDesiredLocationHome()
    {
        desiredPosition = new Vector3(0, 0, 5);
        Debug.Log("home");
    }
}
