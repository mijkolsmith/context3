using UnityEngine;

public class DialogueStarter : MonoBehaviour
{
	[SerializeField] private string dialogue;
	private void OnCollisionEnter(Collision collision)
	{
		GameManager.Instance.UiManager.StartDorienDialogue(dialogue);
	}
}