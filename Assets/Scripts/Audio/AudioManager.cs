using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] sounds;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            Initialize();
        }
    }

    private void Initialize()
    {
        foreach (var sound in sounds)
        {
            sound.source = this.gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }

        Play("MenuMusic");
    }

    public void Play(string name)
    {
        var sound = Array.Find(sounds, s => s.name == name);
        if(sound != null)
            sound.source.Play();
    }

    public void Play(string name, float pitchShift) {
        var sound = Array.Find(sounds, s => s.name == name);
        if (sound != null) {
            float temp = sound.source.pitch;
            sound.source.pitch += pitchShift;
            sound.source.Play();
            sound.source.pitch = temp;
        }
    }

    public void Stop(string name)
    {
        var sound = Array.Find(sounds, s => s.name == name);
        sound?.source.Stop();
    }
}
