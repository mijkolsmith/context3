using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
	[SerializeField] private Image transition;

	private static MenuManager instance = null;
	public static MenuManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<MenuManager>();
			}
			return instance;
		}
	}

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		if (instance != this)
		{
			Destroy(gameObject);
		}
	}

	public void NextScene()
	{
		StartCoroutine(SlowLoadScene(SceneManager.GetActiveScene().buildIndex + 1));
	}

	public IEnumerator SlowLoadScene(int scene)
	{
		for (int i = 0; i < 100; i++)
		{
			transition.color = new Color(1 - i / 100f, 1 - i / 100f, 1 - i / 100f, i / 100f);
			yield return new WaitForSeconds(0.01f);
		}

		SceneManager.LoadScene(scene);
		yield return null;
	}

	public void Quit()
	{
		Application.Quit();
	}
}