using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Volume : MonoBehaviour
{
	Slider slider;
	public AudioMixer mixer;
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
		slider.value = PlayerPrefs.GetFloat("Volume", 0.6f);
		mixer.SetFloat("MasterVol", Mathf.Log10(slider.value + .1f) * 45 + 15);
	}

	/// <summary>
	/// Set a new volume value.
	/// </summary>
	public void SetValue()
	{
		if (slider.value != 0)
		{
			mixer.SetFloat("MasterVol", Mathf.Log10(slider.value + .1f) * 45 + 15);
		}
		else
		{
			mixer.SetFloat("MasterVol", -60);
		}
		PlayerPrefs.SetFloat("Volume", slider.value);
	}
}
