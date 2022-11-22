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
    private NavMeshAgent agent;

    [Header("State")]
    [SerializeField, ReadOnly] private PlayerStates playerState = PlayerStates.idle; //Player is currently... (insert state here)

    [Header("Interaction")]
    [ReadOnly] private IInteractable interactableObject;
    [SerializeField] private KeyCode interactionKey = KeyCode.Mouse1;
    [SerializeField] private LayerMask interactableLayer;

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

    /// <summary>
    /// Get the navMeshAgent in the Start method.
    /// </summary>
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    /// <summary>
    /// Check for movement input, interacion input, crafting input or respawn input in the Update method.
    /// If there's movement input, apply the new target position to the agent.
    /// If there's interaction or crafting input, open the respective popup window if applicable.
    /// If there's respawn input, reset the player's position.
    /// </summary>
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
                agent.SetDestination(hit.point);
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
                    //TODO: fix bug where it doesn't pick a new interactable object
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

    /// <summary>
    /// Get the new interactable object if it enters the trigger collider on this object.
    /// Is the new colliding object interactable? Save it, otherwise keep what was saved previously.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        interactableObject = other.GetComponent<IInteractable>() != null ? other.GetComponent<IInteractable>() : interactableObject;

        //Debug info updater
        if (showDebugInfo)
        {
            interactableGameObject = other.GetComponent<IInteractable>() != null ? other.gameObject : interactableGameObject;
        }
    }

    /// <summary>
    /// Is the colliding object interactable & do we not have an interactable object saved yet? Save it, otherwise keep what was saved previously.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if (interactableObject == null && other.GetComponent<IInteractable>() != null)
        {
            interactableObject = other.GetComponent<IInteractable>();
        }
    }

    /// <summary>
    /// Was the colliding object interactable? Remove the current saved interactable object.
    /// </summary>
    /// <param name="other"></param>
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