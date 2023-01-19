using UnityEngine;

public class ChangeLighting : MonoBehaviour
{
	GameObject[] lighting;
	private void OnTriggerEnter(Collider other)
	{
		lighting = GameManager.Instance.EnvironmentManager.GetCurrentEnvironment().lighting;
		if (lighting?.Length > 1)
		{
			lighting[0].SetActive(lighting[0].activeInHierarchy);
			lighting[1].SetActive(lighting[1].activeInHierarchy);
		}
	}
}