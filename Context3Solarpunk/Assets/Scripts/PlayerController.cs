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
    [SerializeField] private float horizontalMovementSpeed = 5f; //Walking speed side to side
    [SerializeField, EnableIf("canMoveInThreeDimensions")] private float verticalMovementSpeed = 5f; //Walking speed up and down

    [Header("State")]
    [SerializeField, ReadOnly] private PlayerStates playerState = PlayerStates.idle; //Player is currently... (insert state here)

    [Header("Interaction")]
    [ReadOnly] private IInteractable interactableObject;
    [SerializeField] private KeyCode interactionKey = KeyCode.E;
    [SerializeField] private float interactionTimeNeeded = 1f;
    [SerializeField] private GameObject pickupBeamPrefab;
    [SerializeField, ReadOnly] private GameObject pickupBeam;

    [Header("Crafting")]
    [SerializeField] private KeyCode craftingKey = KeyCode.C;
    [SerializeField] private float craftingTimeCooldown = .5f;

    [Header("Crafting")]
    [SerializeField] private KeyCode respawnKey = KeyCode.F5;
    [SerializeField] private Vector3 respawnLocation = Vector3.zero;

    [Space]
    [SerializeField] private bool showDebugInfo = true;
    [Header("Debug")]
    [SerializeField, ReadOnly, ShowIf("showDebugInfo")] private bool interactionInput;
    [SerializeField, ReadOnly, ShowIf("showDebugInfo")] private float interactionTimer;
    [SerializeField, ReadOnly, ShowIf("showDebugInfo")] private bool craftingInput;
    [SerializeField, ReadOnly, ShowIf("showDebugInfo")] private float craftingTimer;
    [SerializeField, ReadOnly, ShowIf("showDebugInfo")] private GameObject interactableGameObject;
    [SerializeField, ReadOnly, ShowIf("showDebugInfo")] private bool respawnInput;

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
        // Movement
        // TODO: for some reason, you can walk in between colliders, we should fix this in the future

        //if (canMoveInThreeDimensions) depthInput = Input.GetAxisRaw("Vertical") else depthInput = 0;
        depthInput = canMoveInThreeDimensions ? Input.GetAxisRaw("Vertical") : 0; //If can move in 3D, 
        movementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0 , depthInput); //Set movementVector

        if (movementInput != Vector3.zero)
        {
            // TODO: should be normalized, otherwise diagonal walking is always fastest, but how do you get different movement speeds then?
            movementVector = new Vector3(MovementInput.x * horizontalMovementSpeed, 0f, MovementInput.z * verticalMovementSpeed) * Time.fixedDeltaTime;
            playerState = PlayerStates.walking;
        } 
        else
        {
            movementVector = Vector3.zero;
            playerState = PlayerStates.idle;
        }
        Move();

        // Interaction
        interactionInput = Input.GetKey(interactionKey);
        if (interactionInput && interactableObject != null)
        {
            if (pickupBeam == null) pickupBeam = Instantiate(pickupBeamPrefab, interactableGameObject.transform.position, Quaternion.LookRotation(transform.position - interactableGameObject.transform.position), transform);

            interactionTimer += Time.deltaTime;
            if (interactionTimer > interactionTimeNeeded)
            {
                Destroy(pickupBeam);
                interactionTimer = 0;
                interactableObject.Interact();
                interactableObject = null;
            }
            //TODO: highlight object or display "Press "E" to interact."
        }
        else
        {
            interactionTimer = 0;
            Destroy(pickupBeam);
        }

        // Crafting menu
        craftingInput = Input.GetKey(craftingKey);
        if (craftingTimer <= craftingTimeCooldown)
		{
            craftingTimer += Time.deltaTime;
        }
        if (craftingInput && (craftingTimer >= craftingTimeCooldown))
        {
            {
                GameManager.Instance.TogglePopupWindow(PopupWindowType.Crafting);
                craftingTimer = 0;
            }
        }

        // Respawning
        respawnInput = Input.GetKey(respawnKey);
        if (respawnInput)
        {
            transform.position = respawnLocation;
        }
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