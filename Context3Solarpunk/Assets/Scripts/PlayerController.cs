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

    [Header("Interaction")]
    [ReadOnly] private IInteractable interactableObject;
    [SerializeField] private float interactionTimeNeeded = 1f;

    [Space]
    [SerializeField] private bool showDebugInfo = true;
    [Header("Debug")]
    [SerializeField, ReadOnly, ShowIf("showDebugInfo")] private bool interactionInput;
    [SerializeField, ReadOnly, ShowIf("showDebugInfo")] private float interactionTimer;
    [SerializeField, ReadOnly, ShowIf("showDebugInfo")] private GameObject interactableGameObject;

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

        interactionInput = Input.GetKey(KeyCode.E);
        if (interactionInput && interactableObject != null)
		{
            //play interaction animation
            interactionTimer += Time.deltaTime;
            if (interactionTimer > interactionTimeNeeded)
			{
                interactionTimer = 0;
                interactableObject.Interact();
                interactableObject = null;
			}
            //TO DO: highlight object or display "Press "E" to interact."
		}
        else interactionTimer = 0;
    }

    private void Move()
    {
        rb.MovePosition(transform.position + movementVector);
    }

    private void OnTriggerEnter(Collider other)
    {
        //is the colliding object interactable? save it, otherwise keep what was saved previously
        interactableObject = other.GetComponent<IInteractable>() != null ? other.GetComponent<IInteractable>() : interactableObject;
        
        //Debug info updater
        if (showDebugInfo)
        {
            interactableGameObject = other.GetComponent<IInteractable>() != null ? other.gameObject : interactableGameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //is the colliding object interactable? save it, otherwise keep what was saved previously
        if (interactableObject == null && other.GetComponent<IInteractable>() != null)
        {
            interactableObject = other.GetComponent<IInteractable>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //is the colliding object interactable? remove it
        interactableObject = other.GetComponent<IInteractable>() != null ? null : interactableObject;

        //Debug info updater
        if (showDebugInfo)
        {
            interactableGameObject = other.GetComponent<IInteractable>() != null ? null : interactableGameObject;
        }
    }
}