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
    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private Color canInteractColor = Color.green;
    [SerializeField] private Color cantInteractColor = Color.red;

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
        if (GameManager.Instance.UiManager.popupWindowOpenType == PopupWindowType.None)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Movement
            movementInput = Input.GetKey(moveKey);
            if (movementInput)
            {
                if (Physics.Raycast(ray, out RaycastHit targetHit, Mathf.Infinity, walkableLayer))
                {
					targetDestinationGameObject.transform.position = targetHit.point;
					agent.SetDestination(targetHit.point);
				}
            }

            // Interaction
            interactionInput = Input.GetKey(interactionKey);

			if (Physics.Raycast(ray, out RaycastHit interactionHit, Mathf.Infinity, interactableLayer))
			{
                interactableObject = interactionHit.collider.GetComponent<IInteractable>();
                interactableGameObject = interactionHit.collider.gameObject;

                if (Vector3.Distance(interactionHit.collider.transform.position, transform.position) < interactionDistance)
				{
					interactableObject.Highlight(canInteractColor);
					if (interactionInput)
					{
						interactableObject.Interact();
						GameManager.Instance.RefreshNavMesh();
					}
				}
				else interactableObject.Highlight(cantInteractColor);
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
}