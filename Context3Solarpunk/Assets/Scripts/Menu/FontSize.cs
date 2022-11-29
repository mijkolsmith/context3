using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FontSize : MonoBehaviour
{
    Slider slider;

    /// <summary>
    /// Get the slider component in the Awake method.
    /// </summary>
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    /// <summary>
	/// Load starting variables from player preferences or set defaults.
	/// </summary>
    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat("FontSize", 1f);
    }

	/// <summary>
	/// Set a new TextSize value.
	/// </summary>
	public void SetValue()
	{
		GameManager.Instance.UiManager.FontSizeModifier = slider.value;
		PlayerPrefs.SetFloat("FontSize", slider.value);
	}
}