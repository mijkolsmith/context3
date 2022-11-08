using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public enum MovementState
{
    isStandingStill = 0,
    isAccelerating = 1,
    isGoing = 2,
    isBraking = 3
}

public class EnvironmentManager : MonoBehaviour
{
    [SerializeField] private GameObject landscapePrefab;
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private GameObject trashPrefab;
    [SerializeField] private int amountOfTiles = 5;

    [SerializeField] private MovementState movementState = MovementState.isStandingStill;

    //For now this class is written in 2D, an eventual 3D implementation is possible in the future
    [SerializeField] private List<GroundObject> groundObjects = new List<GroundObject>();

    [SerializeField] private float maxSpeed = 5.0f;
    [SerializeField] private float acceleration = 0.01f;

    [SerializeField, ReadOnly] private float currentSpeed = 0;

    private Vector3 wrapPosition;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < amountOfTiles; i++)
        {
            GameObject tile = Instantiate(landscapePrefab, new Vector3(0/*i * 5*/, 0, 0), Quaternion.identity, transform);
            GroundObject groundObject = tile.GetComponent<GroundObject>();
            groundObjects.Add(groundObject);
            if (i == 0)
            {
                tile.transform.position = new Vector3(0, 0, 0);
            }
            else
            {
                tile.transform.position = new Vector3(i * groundObject.RightAnchor.transform.position.x, 0, 0);
            }
        }
        if (groundObjects.Count > 0)
        {
            wrapPosition = new Vector3((groundObjects[groundObjects.Count - 1].transform.position.x / 2) - 4, 0, 0);// groundObjects[groundObjects.Count - 1].transform.position;
            gameObject.transform.position -= new Vector3(wrapPosition.x / 2, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveTerrain();
    }

    private void MoveTerrain()
    {
        switch (movementState)
        {
            case MovementState.isStandingStill:
                currentSpeed = 0;
                break;
            case MovementState.isAccelerating:
                if (currentSpeed > (maxSpeed * -1))
                {
                    currentSpeed += acceleration;
                }
                else
                {
                    movementState = MovementState.isGoing;
                    GameManager.Instance.GameStateManager.SetState(new OnMovingTrainState());
                }
                break;
            case MovementState.isGoing:
                currentSpeed = (maxSpeed * -1);
                break;
            case MovementState.isBraking:
                if (currentSpeed < 0)
                {
                    currentSpeed -= acceleration;
                }
                else
                {
                    movementState = MovementState.isStandingStill;
                    GameManager.Instance.GameStateManager.SetState(new OnTrainState());
                }
                break;
            default:
                break;
        }

        for (int i = 0; i < groundObjects.Count; i++)
        {
            groundObjects[i].transform.position += new Vector3(currentSpeed, 0, 0);
            if (groundObjects[i].transform.position.x < ((wrapPosition.x / 2) * -1))
            {
                groundObjects[i].transform.position = wrapPosition;
            }
        }
    }

    [Button]
    public void ToggleTrain()
    {
        if (movementState == MovementState.isStandingStill)
        {
            movementState = MovementState.isAccelerating;
        }
        else
        {
            movementState = MovementState.isBraking;
        }
    }

    public void SetToMoveInSeconds(float seconds)
	{

	}
}
