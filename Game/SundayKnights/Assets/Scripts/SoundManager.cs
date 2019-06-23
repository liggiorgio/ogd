using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    The sound manager is in charge of playing sound effects depending on what
    is happening during the game: mainly, it plays the sfx for tile matches.
*/

public class SoundManager : MonoBehaviour
{
    public AudioClip[] matchAudioClip;
    public AudioClip effectBomb, effectJuice, effectJelly, effectCake;
    public AudioClip tickTock;
    public AudioClip musicBackground, musicStart, musicWin, musicLose;
    AudioSource source, time;

    // Awake is called after creation
    void Awake()
    {
        source = AddAudio();
        time = AddLoop(tickTock);
    }

    void Start()
    {
        source.PlayOneShot(musicStart, .75f);
        source.PlayOneShot(musicBackground, .75f);
    }

    AudioSource AddAudio()
    {
        AudioSource audioSource = this.gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        return audioSource;
    }

    AudioSource AddLoop(AudioClip audioClip)
    {
        AudioSource audioSource = this.gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = audioClip;
        audioSource.loop = true;
        return audioSource;
    }

    public void PlayMatch(int i)
    {
        if (i > 4)
            i = 4;
        source.PlayOneShot(matchAudioClip[i], 1f);
    }

    public void PlayEndMusic(bool won)
    {
        if (won)
            source.PlayOneShot(musicWin, 1f);
        else
            source.PlayOneShot(musicLose, 1f);
    }

    public void PlayTime()
    {
        time.Play();
    }

    public void StopTime()
    {
        time.Stop();
    }

    public void PlayBomb()
    {
        source.PlayOneShot(effectBomb, 1f);
    }

    public void PlayJuice()
    {
        source.PlayOneShot(effectJuice, 1f);
    }

    public void PlayJelly()
    {
        source.PlayOneShot(effectJelly, 1f);
    }

    public void PlayCake()
    {
        source.PlayOneShot(effectCake, 1f);
    }
}
