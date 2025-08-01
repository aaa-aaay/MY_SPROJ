using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    // Pool for AudioSources
    private Queue<AudioSource> audioSourcePool = new Queue<AudioSource>();
    [SerializeField] private int poolSize = 10;

    // AudioSource just for background music
    private AudioSource backgroundMusicSource;
    private Dictionary<string, Sound> soundDictionary = new Dictionary<string, Sound>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            for (int i = 0; i < poolSize; i++)
            {
                AudioSource source = gameObject.AddComponent<AudioSource>();
                source.spatialBlend = 1.0f;
                source.rolloffMode = AudioRolloffMode.Logarithmic;
                source.minDistance = 1.0f;
                source.maxDistance = 30.0f;
                audioSourcePool.Enqueue(source);
            }
        }
        else
        {
            Destroy(gameObject);
        }

        LoadAllSounds();


        backgroundMusicSource = gameObject.AddComponent<AudioSource>();
        backgroundMusicSource.loop = true;
        backgroundMusicSource.spatialBlend = 0.0f;
        backgroundMusicSource.playOnAwake = false;
        PlayBackgroundMusic("BGM1");
    }

    public void PlaySFX(string name, Vector3 position = default)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.name == name)
            {
                // Get an available AudioSource from the pool
                if (audioSourcePool.Count > 0)
                {
                    AudioSource source = audioSourcePool.Dequeue();
                    if (source == null) return;
                    source.clip = sound.clip;
                    source.volume = sound.volume;
                    if(position != default)
                    {
                        source.transform.position = position;
                    }

                    source.Play();

                    StartCoroutine(ReturnToPoolAfterPlayback(source));
                }
                else
                {
                    Debug.Log("No audio sources !!!!");
                }
                return;
            }
        }

    }

    public void PlayBackgroundMusic(string name)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.name == name)
            {
                Debug.Log("Starting BGM:" +  sound.name);
                backgroundMusicSource.clip = sound.clip;
                backgroundMusicSource.volume = sound.volume;
                backgroundMusicSource.Play();
                return;
            }
        }

        Debug.Log("BGM " + name + " not found");
    }

    private IEnumerator ReturnToPoolAfterPlayback(AudioSource source)
    {
        yield return new WaitForSeconds(source.clip.length);
        source.Stop();
        source.clip = null;
        audioSourcePool.Enqueue(source);
    }



    private void LoadAllSounds()
    {
        sounds = Resources.LoadAll<Sound>("AudioSO"); // make sure Sound SO is in this directory
        foreach (Sound sound in sounds)
        {
            if (!soundDictionary.ContainsKey(sound.name))
            {
                soundDictionary.Add(sound.name, sound);
            }
            else
            {
                Debug.LogWarning($"Duplicate sound name found: {sound.name}. Skipping.");
            }
        }
    }


    public void StopSounds(bool stopAll = true, int stopWhich = 1)
    {
        if (stopAll || stopWhich == 1)
        {
            if (backgroundMusicSource != null && backgroundMusicSource.isPlaying)
            {
                backgroundMusicSource.Stop();

            }
        }

        if (stopAll || stopWhich == 2)
        {
            List<AudioSource> activeSources = new List<AudioSource>();

            while (audioSourcePool.Count > 0)
            {
                Debug.Log("things in the pool");
                AudioSource source = audioSourcePool.Dequeue();
                if (source != null && source.isPlaying)
                {
                    source.Stop();
                    source.clip = null;
                    Debug.Log("Audio Stopeed");
                }
                activeSources.Add(source);
            }

            foreach (var source in activeSources)
            {
                audioSourcePool.Enqueue(source);
            }
        }
    }


    public void DestoryGameObject() { Destroy(gameObject); }

}
