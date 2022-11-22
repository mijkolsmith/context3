using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//TODO: Cleanup / Remove all unnecessary code, remove this script?
public class TrainManager : MonoBehaviour
{
    [SerializeField] private Transform stationLocation; //Could be done with Vector3 but transforms are easier
    [SerializeField] private Vector3 desiredPosition;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private bool playerInTrain = false;
    [SerializeField] private GameObject trashcanPrefab;
    [SerializeField] private Material[] trashcanMaterials;

    private Vector3 velocity = Vector3.zero;

    public Train train;

    private void Start()
    {
        InstantiateWagons();
        //SetDesiredLocationHome(); //This is to make the train drive into the scene at the start
    }

    private void Update()
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, step);
        CheckIfPlayerOnTrain();
        //only move in OnMovingTrainState
    }

    private void InstantiateWagons()
    {
        //desiredPosition = stationLocation.position;

        for (int i = 0; i < train.wagons.Count; i++)
        {
            if (i == 0)
            {
                WagonReference wr = train.wagons[i];
                wr.wagonObject = Instantiate(train.wagons[i].wagonObject, stationLocation);
                wr.wagon = wr.wagonObject.GetComponent<Wagon>();
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
                wr.wagon = wr.wagonObject.GetComponent<Wagon>();
                train.wagons[i] = wr;
            }
        }
    }

    //TODO: remove code below and make it actually good
    public void SetDesiredLocationLeft()
    {
        desiredPosition = new Vector3(-100, 0, 7.8f);
        Debug.Log("left");
    }

    public void SetDesiredLocationRight()
    {
        desiredPosition = new Vector3(100, 0, 7.8f);
        Debug.Log("right");
    }

    public void SetDesiredLocationHome()
    {
        desiredPosition = new Vector3(0, 0, 7.8f);
        Debug.Log("home");
    }

    public void BringTrainToBlockoutStation() //Heb even een nieuwe method hiervoor aangemaakt zodat ik niks zou slopen
    {
        desiredPosition = new Vector3(-71.5f, 1.87f, 17.33f);
    }

    public void CheckIfPlayerOnTrain()
    {
        int x = 0;
        for (int i = 0; i < train.wagons.Count; i++)
        {
            if (train.wagons[i].wagonObject.GetComponent<Wagon>().HasPlayerInside)
            {
                x++;
            }
        }

        if (x > 0) //has player inside
        {
            if (playerInTrain == false)
            {
                playerInTrain = true;
                GameManager.Instance.GameStateManager.SetState(new OnTrainState());
                Debug.Log("Player on train, current state: " + GameManager.Instance.GameStateManager.GetState());
            }
        }
        else
        {
            if (playerInTrain == true)
            {
                playerInTrain = false;
                GameManager.Instance.GameStateManager.SetState(new InPastOnPlatformState()); //TODO: stop creating new ones every frame
                Debug.Log("Player on platform, current state: " + GameManager.Instance.GameStateManager.GetState());
            }
        }
    }
}