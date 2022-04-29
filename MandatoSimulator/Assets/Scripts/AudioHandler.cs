using System.Collections;
using System.Collections.Generic;
using Padoru.Core;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    public AudioSource source;

    private void Awake()
    {
        Locator.RegisterService(this);
    }

    private void OnDestroy()
    {
        Locator.UnregisterService<AudioHandler>();
    }

    public void PlayAudioClip(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
