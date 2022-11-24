using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;

public abstract class GatherableResource : Resource, IInteractable, IGatherable
{
	[SerializeField, ReadOnly] private Collider hitboxCollider;
	[SerializeField, ReadOnly] private NavMeshObstacle navMeshObstacle;
	[SerializeField, ReadOnly] private Vector3 startPos;
	[SerializeField, ReadOnly] private float timeElapsed = 0f;
	[SerializeField] private float lerpDuration = 3f;

	/// <summary>
	/// Assign some components in the start method
	/// </summary>
	private void Start()
	{
		hitboxCollider = GetComponent<Collider>();
		navMeshObstacle = GetComponent<NavMeshObstacle>();
		startPos = transform.position;
	}

	/// <summary>
	/// Lerp the Resource to it's starting position in the Update method if the Resource is not being interacted with
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
		else if (timeElapsed >= lerpDuration)
		{
			transform.position = startPos;
			hitboxCollider.isTrigger = false;
			navMeshObstacle.enabled = true;
		}
	}

	/// <summary>
	/// Add the GatherableResource to the inventory.
	/// </summary>
	public virtual void Gather()
	{
		GameManager.Instance.CraftingManager.AddResourceToInventory(GetResourceType());
	}

	/// <summary>
	/// Remove the trash from the count, and advance eventual quest tasks. 
	/// Deactivates quest objects in the QuestManager.
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

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Dorien"))
		{
			Debug.Log("onTriggerEnter");
			Gather();
			GameManager.Instance.trashCount--;

			GameManager.Instance.QuestManager.AdvanceTasks();
		}
	}
}