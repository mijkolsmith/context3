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
    [SerializeField] private float movementSpeed = 5f; //Walking speed side to side
    [SerializeField] private float playerGravity = -9.81f;

    [Header("State")]
    [SerializeField, ReadOnly] private PlayerStates playerState = PlayerStates.idle; //Player is currently... (insert state here)

    [Header("Interaction")]
    [ReadOnly] private IInteractable interactableObject;
    [SerializeField] private KeyCode interactionKey = KeyCode.E;
    [SerializeField] private float interactionTimeNeeded = 1f;

    [Header("Crafting")]
    [SerializeField] private KeyCode craftingKey = KeyCode.C;
    [SerializeField] private float craftingTimeCooldown = .5f;

    [Header("Crafting")]
    [SerializeField] private KeyCode respawnKey = KeyCode.F5;
    [SerializeField] private Vector3 respawnLocation = Vector3.zero;

    [Space]
    [SerializeField] private GameObject companionPositionGameObject;
    [Header("Debug")]

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
    private CharacterController charController;

    #endregion

    #region Properties
    private Vector3 MovementInput { get => movementInput; set => movementInput = value; }
    public float HorizontalMovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    internal PlayerStates PlayerState { get => playerState; set => playerState = value; }
    public GameObject CompanionPositionGameObject { get => companionPositionGameObject; set => companionPositionGameObject = value; }
    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        charController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Movement
        // TODO: for some reason, you can walk in between colliders, we should fix this in the future

        //if (canMoveInThreeDimensions) depthInput = Input.GetAxisRaw("Vertical") else depthInput = 0;
        depthInput = canMoveInThreeDimensions ? Input.GetAxisRaw("Vertical") : 0; //If can move in 3D, 
        movementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, depthInput); //Set movementVector

        
        Move();

        // Interaction
        interactionInput = Input.GetKey(interactionKey);
        if (interactableObject != null)
        {
            if (interactionInput)
            {
                //TODO: play interaction animation
                interactionTimer += Time.deltaTime;
                if (interactionTimer > interactionTimeNeeded)
                {
                    interactionTimer = 0;
                    interactableObject.Interact();
                    interactableObject = null;
                }
            }
            GameManager.Instance.UiManager.CanInteractPopupUIObject.SetActive(true);
        }
        else
        {
            GameManager.Instance.UiManager.CanInteractPopupUIObject.SetActive(false);
            interactionTimer = 0;
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

        if (movementInput != Vector3.zero)
        {
            // TODO: should be normalized, otherwise diagonal walking is always fastest, but how do you get different movement speeds then?
            movementVector = new Vector3(MovementInput.x * movementSpeed, 0f, MovementInput.z * movementSpeed) * Time.fixedDeltaTime;
            playerState = PlayerStates.walking;
        }
        else
        {
            movementVector = Vector3.zero;
            playerState = PlayerStates.idle;
        }
        //TODO: Make this addforce or charactercontroller based movement
        var mvmnt = movementVector * Time.deltaTime * movementSpeed;
        var grvty = playerGravity * Time.deltaTime;
        charController.Move(new Vector3(mvmnt.x, grvty, mvmnt.z));
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