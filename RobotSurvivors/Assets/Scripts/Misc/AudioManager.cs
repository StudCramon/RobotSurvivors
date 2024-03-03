using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.Rendering;
using Unity.VisualScripting;

public enum SoundNames
{
    LASERSHOT,
    HITSOUND,
    EXPLOSION
}

public class AudioManager : MonoBehaviour
{
    [SerializeField] Sound[] sounds;

    public float currentValue = 1.0f;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.name = s.name;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void PlaySound(SoundNames soundName)
    {
        sounds[(int)soundName].source.Play();
    }

    public void ChangeVolume(float value)
    {
        currentValue = value;
        foreach (Sound s in sounds)
        {
            s.source.volume = value;
        }
    }
}
