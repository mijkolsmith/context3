using UnityEngine;

public class ChangeLighting : MonoBehaviour
{
	GameObject[] lighting;
	private void OnTriggerEnter(Collider other)
	{
		lighting = GameManager.Instance.EnvironmentManager.GetCurrentEnvironment().lighting;
		if (lighting?.Length > 1)
		{
			lighting[0].SetActive(false);
			lighting[1].SetActive(true);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (lighting?.Length > 1)
		{
			lighting[0].SetActive(true);
			lighting[1].SetActive(false);
		}
	}
}