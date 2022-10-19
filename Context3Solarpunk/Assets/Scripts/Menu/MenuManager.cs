using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
	[SerializeField] private Image transition;
	[SerializeField] private GameObject[] menus;
	[SerializeField] private GameObject[] SelectOrStartButtons;
	
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

	public void ToggleCharacterSelectionMenu()
	{
		if (menus[0].activeInHierarchy)
		{
			menus[1].SetActive(true);
			menus[0].SetActive(false);
		}
		else
		{
			menus[0].SetActive(true);
			menus[1].SetActive(false);
		}
	}

	public void NextScene()
	{
		//TODO: Save which character is chosen and then disable the character selection screen
		//If character is chosen enable SelectOrStartButtons[1] and disable SelectOrStartButtons[0] (playerprefs)
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