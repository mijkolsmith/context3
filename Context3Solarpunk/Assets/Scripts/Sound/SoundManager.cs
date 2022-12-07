using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public SoundClass[] sounds;

    /// <summary>
    /// Play a one-shot sound by name
    /// </summary>
    /// <param name="name"></param>
    public void PlayOneShotSound(SoundName name)
    {
        audioSource.PlayOneShot(sounds.Where(x => x.soundName == name).FirstOrDefault().audioClip);
    }

    /// <summary>
    /// Play a sound constantly
    /// </summary>
    /// <param name="name"></param>
    public void PlaySound(SoundName name)
    {
        audioSource.clip = sounds.Where(x => x.soundName == name).FirstOrDefault().audioClip;
        audioSource.Play();
    }

    /// <summary>
    /// Pause playing sound
    /// </summary>
    public void PauseSound()
    {
        audioSource.Pause();
    }

    /// <summary>
    /// Stop playing sound
    /// </summary>
    public void StopSound()
    {
        audioSource.Stop();
    }
}