using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("General Sounds")]
    public Sound[] sfxSounds;
    public Sound[] musicTracks;

    private Dictionary<string, AudioClip> sfxDictionary = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> musicDictionary = new Dictionary<string, AudioClip>();

    private AudioSource musicSource;
    private List<AudioSource> activeSFXSources = new List<AudioSource>();

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float sfxVolume = 1f;
    [Range(0f, 1f)] public float musicVolume = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudioManager();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeAudioManager()
    {
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.volume = musicVolume;

        foreach (var sound in sfxSounds)
            sfxDictionary[sound.name] = sound.clip;

        foreach (var music in musicTracks)
            musicDictionary[music.name] = music.clip;
    }

    public void PlaySFX(string name)
    {
        if (sfxDictionary.TryGetValue(name, out AudioClip clip))
        {
            AudioSource sfx = gameObject.AddComponent<AudioSource>();
            sfx.clip = clip;
            sfx.volume = sfxVolume;
            sfx.Play();
            activeSFXSources.Add(sfx);
            StartCoroutine(DestroySourceWhenDone(sfx));
        }
        else
        {
            Debug.LogWarning("SFX not found: " + name);
        }
    }

    public void PlayMusic(string name, bool fade = true, float fadeTime = 1f)
    {
        if (musicDictionary.TryGetValue(name, out AudioClip clip))
        {
            if (fade)
            {
                StartCoroutine(FadeMusic(clip, fadeTime));
            }
            else
            {
                musicSource.clip = clip;
                musicSource.volume = musicVolume;
                musicSource.Play();
            }
        }
        else
        {
            Debug.LogWarning("Music not found: " + name);
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    private System.Collections.IEnumerator FadeMusic(AudioClip newClip, float duration)
    {
        float startVolume = musicSource.volume;

        // Fade out
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(startVolume, 0, t / duration);
            yield return null;
        }

        musicSource.clip = newClip;
        musicSource.Play();

        // Fade in
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(0, musicVolume, t / duration);
            yield return null;
        }

        musicSource.volume = musicVolume;
    }

    private System.Collections.IEnumerator DestroySourceWhenDone(AudioSource source)
    {
        yield return new WaitWhile(() => source.isPlaying);
        activeSFXSources.Remove(source);
        Destroy(source);
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        musicSource.volume = volume;
    }
}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}
