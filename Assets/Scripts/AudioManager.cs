using UnityEngine;
using System.Collections.Generic;

/*
 * Check out Brackeys Video:
 * Introduction to AUDIO in Unity
 * https://www.youtube.com/watch?v=6OT43pvUyfY
 */

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] sounds;

    private Dictionary<string, Sound> soundsDict;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        soundsDict = new Dictionary<string, Sound>();

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            soundsDict.Add(s.name, s);
        }
    }

    //Only plays sound if not already playing
    public void PlayWithoutOverlap(string name)
    {
        if (soundsDict[name].source.isPlaying)
            return;
        else
            soundsDict[name].source.Play();
    }

    //Plays a sound even if the same sound is currently playing
    public void PlayOverlapping(string name)
    {
        soundsDict[name].source.PlayOneShot(soundsDict[name].source.clip);
    }

    public void Stop(string name)
    {
        soundsDict[name].source.Stop();
    }
}

