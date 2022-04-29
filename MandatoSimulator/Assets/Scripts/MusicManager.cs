using System.Collections;
using System.Collections.Generic;
using Padoru.Core;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    [System.Serializable]
    private class MusicSettings
    {
        public AudioClip clip;
        [Range(0, 1)] public float volume;
    }

    [SerializeField] private List<MusicSettings> musicSettings;
    [SerializeField] private float fadeTime = 1f;

    private int currentClipIndex;
    private AudioSource audioSource;
    private Coroutine fadeCoroutine;
    
    private void Awake()
    {
        try
        {
            if (Locator.GetService<MusicManager>() != null)
            {
                Destroy(gameObject);
                return;
            }
        }
        catch
        {
            
        }
        
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();

        Locator.RegisterService(this);

        PlayNextClip();
    }

    private void PlayNextClip()
    {
        var settings = musicSettings[currentClipIndex];

        currentClipIndex = (currentClipIndex + 1) % musicSettings.Count;

        audioSource.Stop();
        audioSource.clip = settings.clip;
        audioSource.volume = 0;
        audioSource.Play();

        StartFadeIn(settings.volume);
        Invoke(nameof(StartFadeOut), settings.clip.length - fadeTime);
        Invoke(nameof(PlayNextClip), settings.clip.length);
    }

    private void StartFadeIn(float targetVolume)
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        fadeCoroutine = StartCoroutine(FadeCoroutine(targetVolume));
    }

    private void StartFadeOut()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        
        fadeCoroutine = StartCoroutine(FadeCoroutine(0));
    }

    private IEnumerator FadeCoroutine(float targetVolume)
    {
        var timer = 0f;
        var initialVolume = audioSource.volume;

        while (timer <= fadeTime)
        {
            timer += Time.deltaTime;
            var percent = timer / fadeTime;

            audioSource.volume = Mathf.Lerp(initialVolume, targetVolume, percent);

            yield return null;
        }
    }
}
