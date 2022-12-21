using NaughtyAttributes;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class SoundManager : MonoBehaviour
{
    [SerializeField, ReadOnly] private AudioSource[] audioSources;
    public SoundClass[] sounds;

    /// <summary>
    /// Get all the attached audioSources 
    /// You need 2 sources for the script to function
    /// </summary>
	private void Awake()
	{
        audioSources = GetComponents<AudioSource>();
	}

	/// <summary>
	/// Play a one-shot sound by name
	/// </summary>
	/// <param name="name"></param>
	public void PlayOneShotSound(SoundName name)
    {
        audioSources[0].PlayOneShot(sounds.Where(x => x.soundName == name).FirstOrDefault().audioClip);
    }

    /// <summary>
    /// Play a sound constantly
    /// </summary>
    /// <param name="name"></param>
    public void PlaySound(SoundName name)
    {
        audioSources[1].clip = sounds.Where(x => x.soundName == name).FirstOrDefault().audioClip;
        audioSources[1].Play();
    }

    /// <summary>
    /// Pause playing sound
    /// </summary>
    public void PauseSound()
    {
        audioSources[1].Pause();
    }

    /// <summary>
    /// Stop playing sound
    /// </summary>
    public void StopSound()
    {
        audioSources[1].Stop();
    }
}