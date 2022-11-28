using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;

public class PlayerControllerPointClick : MonoBehaviour
{
    #region Variables
    [Header("Movement")]
    [SerializeField] private KeyCode moveKey = KeyCode.Mouse1; //Als je KeyCode.Mouse gebruikt werkt het niet op tablets, just a headsup
    [SerializeField] private LayerMask walkableLayer;
    [SerializeField] private GameObject targetDestinationGameObject;
    [SerializeField] private float targetGameObjectDisappearDistance = 2f;
    private NavMeshAgent agent;

    [Header("State")]
    [SerializeField, ReadOnly] private PlayerStates playerState = PlayerStates.idle; //Player is currently... (insert state here)

    [Header("Interaction")]
    [ReadOnly] private IInteractable interactableObject;
    [SerializeField] private KeyCode interactionKey = KeyCode.Mouse1; //Als je KeyCode.Mouse gebruikt werkt het niet op tablets, just a headsup
    [SerializeField] private LayerMask interactableLayer;

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
    public static GameObject Player { get; private set; }
    internal PlayerStates PlayerState { get => playerState; set => playerState = value; }
    public GameObject CompanionPositionGameObject { get => companionPositionGameObject; set => companionPositionGameObject = value; }
    public IInteractable InteractableObject { get => interactableObject; set => interactableObject = value; }
    #endregion

    /// <summary>
    /// Get the navMeshAgent in the Start method.
    /// </summary>
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        Player = gameObject;
    }

    /// <summary>
    /// Check for movement input, interacion input, crafting input or respawn input in the Update method.
    /// If there's movement input, apply the new target position to the agent.
    /// If there's interaction or crafting input, open the respective popup window if applicable.
    /// If there's respawn input, reset the player's position.
    /// </summary>
    private void Update()
    {
        // UI Open Check
        if (GameManager.Instance.popupWindowOpenType == PopupWindowType.None)
        {
            // Movement
            movementInput = Input.GetKey(moveKey);
            if (movementInput)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, walkableLayer))
                {
					targetDestinationGameObject.transform.position = hit.point;
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

                    if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, interactableLayer))
                    {
                        if (hit.collider.gameObject.GetComponent<IInteractable>() == interactableObject)
                        {
                            interactableObject.Interact();
                            interactableObject = null;
                            GameManager.Instance.RefreshNavMesh();
                        }
					}
				}
            }

            // Respawning
            respawnInput = Input.GetKeyDown(respawnKey);
            if (respawnInput)
            {
                transform.position = respawnLocation;
            }
        }
        if (Vector3.Distance(gameObject.transform.position, targetDestinationGameObject.transform.position) < targetGameObjectDisappearDistance)
        {
            targetDestinationGameObject.SetActive(false);
        }
        else
        {
            targetDestinationGameObject.SetActive(true);
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
        interactableObject = other.GetComponent<IInteractable>() != null ? null : interactableObject;

        //Debug info updater
        if (showDebugInfo)
        {
            interactableGameObject = other.GetComponent<IInteractable>() != null ? null : interactableGameObject;
        }
    }
}