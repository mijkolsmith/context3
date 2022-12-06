using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;

public abstract class GatherableResource : Resource, IInteractable, IGatherable
{
    [SerializeField, ReadOnly] private Collider hitboxCollider;
    [SerializeField, ReadOnly] private Outline objectOutline;
    [SerializeField, ReadOnly] private NavMeshObstacle navMeshObstacle;
    [SerializeField, ReadOnly] private Vector3 startPos;
    [SerializeField, ReadOnly] private float timeElapsed = 0f;
    [SerializeField] private float lerpDuration = 3f;

    /// <summary>
    /// Assign some components in the start method.
    /// </summary>
    private void Start()
    {
        hitboxCollider = GetComponent<Collider>();
        navMeshObstacle = GetComponent<NavMeshObstacle>();
        objectOutline = GetComponent<Outline>();

        startPos = transform.position;
    }

    /// <summary>
    /// Lerp the Resource to it's starting position in the Update method if the Resource is not being interacted with.
    /// </summary>
    private void Update()
    {
        if (timeElapsed < lerpDuration)
        {
            timeElapsed += Time.deltaTime;
            var t = timeElapsed / lerpDuration;
            t = t * t * (3f - 2f * t);
            transform.position = Vector3.Lerp(transform.position, startPos, t);
        }

        if (timeElapsed >= lerpDuration || Vector3.Distance(transform.position, startPos) < 0.05f)
        {
            timeElapsed = lerpDuration;

            transform.position = startPos;
            hitboxCollider.isTrigger = false;
            navMeshObstacle.enabled = true;

            //Reset outline
            objectOutline.OutlineWidth = 0f;
        }
    }

    /// <summary>
    /// Activate the highlight outline.
    /// </summary>
    /// <param name="color"></param>
    public virtual void Highlight(Color color)
    {
        objectOutline.OutlineWidth = 5f;
        objectOutline.OutlineColor = color;
    }

    /// <summary>
    /// Add the GatherableResource to the inventory.
    /// </summary>
    public virtual void Gather()
    {
        GameManager.Instance.SoundManager.PlayOneShotSound(SoundName.ITEM_PICKUP);
        GameManager.Instance.CraftingManager.AddResourceToInventory(GetResourceType());
    }

    /// <summary>
    /// Drag the trash around on X and Y axis following the mouse, and disable the colliders.
    /// </summary>
    public virtual void Interact()
    {
        timeElapsed = 0;
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Mathf.Abs(Vector3.Distance(Camera.main.transform.position, transform.position))));
        
        gameObject.transform.position = new Vector3(
            mouseWorldPos.x,
            mouseWorldPos.y + .5f,
            transform.position.z);

        hitboxCollider.isTrigger = true;
        navMeshObstacle.enabled = false;
    }

    /// <summary>
    /// If the object is dragged to Dorien, remove the trash from the count, and advance eventual quest tasks. 
    /// Deactivates quest objects in the QuestManager.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dorien") && navMeshObstacle.enabled == false)
        {
            Gather();
            GameManager.Instance.trashCount--;

            //TEMP questmanager doesnt work always yet, so to make testing easier:
            gameObject.SetActive(false);
            GameManager.Instance.QuestManager.AdvanceTasks();
        }
    }
}