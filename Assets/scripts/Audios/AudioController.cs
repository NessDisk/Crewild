using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class AudioController
{
    /// <summary>
    /// decremento del audio
    /// </summary>
    /// <param name="audioSource"></param>
    /// <param name="FadeTime"></param>
    /// <returns></returns>
    public static IEnumerator DecrementoGradual(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
          
            yield return null;
        }
        audioSource.Stop();
    }

    /// <summary>
    /// incremento del Audio
    /// </summary>
    /// <param name="audioSource"></param>
    /// <param name="FadeTime"></param>
    /// <returns></returns>
    public static IEnumerator IncremntoGradual(AudioSource audioSource, float FadeTime)
    {
        audioSource.Play();
        audioSource.volume = 0f;
        while (audioSource.volume < 0.25f)
        {
            audioSource.volume += Time.deltaTime / FadeTime;
            yield return null;
        }
    }
}
