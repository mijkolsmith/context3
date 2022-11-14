using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;

public class PlayerControllerPointClick : MonoBehaviour
{
    #region Variables
    [Header("Movement")]
    [SerializeField] private KeyCode moveKey = KeyCode.Mouse1;
    [SerializeField] private LayerMask walkableLayer;
    [SerializeField] private GameObject targetDestination;
    private NavMeshAgent player;

    [Header("State")]
    [SerializeField, ReadOnly] private PlayerStates playerState = PlayerStates.idle; //Player is currently... (insert state here)

    [Header("Interaction")]
    [ReadOnly] private IInteractable interactableObject;
    [SerializeField] private KeyCode interactionKey = KeyCode.Mouse1;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private float interactionTimeNeeded = 1f;
    [SerializeField] private GameObject pickupBeamPrefab;
    [SerializeField, ReadOnly] private GameObject pickupBeam;

    [Header("Crafting")]
    [SerializeField] private KeyCode craftingKey = KeyCode.C;
    [SerializeField] private float craftingTimeCooldown = .5f;

    [Header("Respawning")]
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

    [SerializeField, ReadOnly, ShowIf("showDebugInfo")] private bool movementInput;
    [SerializeField, ReadOnly, ShowIf("showDebugInfo")] private Vector3 movementVector;

    private Animator anim;

    #endregion

    #region Properties
    internal PlayerStates PlayerState { get => playerState; set => playerState = value; }
    public GameObject CompanionPositionGameObject { get => companionPositionGameObject; set => companionPositionGameObject = value; }
    #endregion

    private void Start()
    {
        player = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Movement
        movementInput = Input.GetKeyDown(moveKey);
        if (movementInput)
		{
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, walkableLayer))
			{
                targetDestination.transform.position = hit.point;
                player.SetDestination(hit.point);
			}
		}

        // Interaction
        interactionInput = Input.GetKey(interactionKey);
        if (interactableObject != null)
        {
            if (interactionInput)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactableLayer))
                {
                    interactableObject.Interact();
                    interactableObject = null;
                }
            }
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

    private void OnTriggerEnter(Collider other)
    {
        //Is the colliding object interactable? save it, otherwise keep what was saved previously
        interactableObject = other.GetComponent<IInteractable>() != null ? other.GetComponent<IInteractable>() : interactableObject;

        //Debug info updater
        if (showDebugInfo)
        {
            interactableGameObject = other.GetComponent<IInteractable>() != null ? other.gameObject : interactableGameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Is the colliding object interactable? save it, otherwise keep what was saved previously
        if (interactableObject == null && other.GetComponent<IInteractable>() != null)
        {
            interactableObject = other.GetComponent<IInteractable>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Is the colliding object interactable? remove it
        interactableObject = other.GetComponent<IInteractable>() != null ? null : interactableObject;

        //Debug info updater
        if (showDebugInfo)
        {
            interactableGameObject = other.GetComponent<IInteractable>() != null ? null : interactableGameObject;
        }
    }
}