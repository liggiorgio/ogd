using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    The sound manager is in charge of playing sound effects depending on what
    is happening during the game: mainly, it plays the sfx for tile matches.
*/

public class SoundManager : MonoBehaviour
{
    public AudioClip matchAudioClip;
    AudioSource match;

    // Awake is called after creation
    void Awake()
    {
        match = AddAudio(matchAudioClip);
    }

    AudioSource AddAudio(AudioClip audioClip)
    {
        AudioSource audioSource = this.gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = audioClip;
        return audioSource;
    }

    public void PlayMatch()
    {
        match.PlayOneShot(matchAudioClip, 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
