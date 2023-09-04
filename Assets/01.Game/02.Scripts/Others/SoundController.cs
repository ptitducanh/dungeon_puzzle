using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.Common;
using UnityEngine;

public class SoundController : Singleton<SoundController>
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;

    private Dictionary<string, AudioClip> _audioClipsMap = new();

    private void Start()
    {
        for (int i = 0; i < audioClips.Length; i++)
        {
            _audioClipsMap.Add(audioClips[i].name, audioClips[i]);
        }
    }

    public void PlaySFX(string name)
    {
        if (_audioClipsMap.ContainsKey(name))
        {
            audioSource.PlayOneShot(_audioClipsMap[name]);
        }
    }
}
