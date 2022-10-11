using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

enum PlayerStates
{
    walking = 0,
    idle = 1
}

public class PlayerController : MonoBehaviour
{
    #region Variables
    [Label("Enable depth movement")]

    [Header("Movement")]
    [SerializeField] bool canMoveInThreeDimensions = false;
    [SerializeField] private float horizontalMovementSpeed = 1f; //Walking speed side to side
    [SerializeField, EnableIf("canMoveInThreeDimensions")] private float verticalMovementSpeed = 1f; //Walking speed up and down

    [Header("State")]
    [SerializeField, ReadOnly] private PlayerStates playerState = PlayerStates.idle; //Player is currently... (insert state here)

    [Space]
    [SerializeField] private bool showDebugInfo = true;
    [Header("Debug")]
    [SerializeField, ReadOnly, ShowIf("showDebugInfo")] private Vector3 movementInput;
    [SerializeField, ReadOnly, ShowIf("showDebugInfo")] private Vector3 movementVector;

    float depthInput = 0;

    private Rigidbody rb;
    private Animator anim;

    #endregion

    #region Properties
    private Vector3 MovementInput { get => movementInput; set => movementInput = value; }
    public float HorizontalMovementSpeed { get => horizontalMovementSpeed; set => horizontalMovementSpeed = value; }
    public float VerticalMovementSpeed { get => verticalMovementSpeed; set => verticalMovementSpeed = value; }
    internal PlayerStates PlayerState { get => playerState; set => playerState = value; }
    #endregion


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //if (canMoveInThreeDimensions) depthInput = Input.GetAxisRaw("Vertical") else depthInput = 0;
        
        depthInput = canMoveInThreeDimensions ? Input.GetAxisRaw("Vertical") : 0; //If can move in 3D, 
        movementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0 , depthInput); //Set movementVector
        if (movementInput != Vector3.zero)
        {
            movementVector = new Vector3(MovementInput.x * horizontalMovementSpeed, 0f, MovementInput.z * verticalMovementSpeed) * Time.fixedDeltaTime;
            playerState = PlayerStates.walking;
        } 
        else
        {
            movementVector = Vector3.zero;
            playerState = PlayerStates.idle;
        }
        Move();
    }

    private void Move()
    {
        rb.MovePosition(transform.position + movementVector);
    }

    private void Interact()
    {

    }
}
