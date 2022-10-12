using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private void Update()
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, step);
        //only move in OnMovingTrainState
    }

    private void InstantiateWagons()
    {
        desiredPosition = stationLocation.position;

        for (int i = 0; i < train.wagons.Count; i++)
        {
            if (i == 0) 
            {
                WagonReference wr = train.wagons[i];
                wr.wagonObject = Instantiate(train.wagons[i].wagonObject, stationLocation);
                train.wagons[i] = wr;
            } 
            else
            {
                WagonReference wr = train.wagons[i];
                wr.wagonObject = Instantiate(train.wagons[i].wagonObject, new Vector3(
                    stationLocation.position.x + (i * 16), 
                    stationLocation.position.y, 
                    stationLocation.position.z), 
                    Quaternion.identity, stationLocation);
                train.wagons[i] = wr;
            }
        }
    }

	public void BreakUnit(int unit)
	{
        if (unit == 0) train.wagons.Where(x => x.wagonType == WagonType.WAGON_HEATING_UNIT).FirstOrDefault().wagonObject.GetComponent<Wagon>().Unit.Break();
        else if (unit == 1) train.wagons.Where(x => x.wagonType == WagonType.WAGON_COOLING_UNIT).FirstOrDefault().wagonObject.GetComponent<Wagon>().Unit.Break();
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