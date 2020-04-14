using UnityEngine;
using System.Collections.Generic;
public interface IAudioManager
{

    bool MusicOn { get; set; }
    AudioSource PlaySound(AudioClip clip, Vector3 position);

    void PlayMusic(AudioClip aClip);


    bool SoundOn { get; set; }
}
public static class Audio
{
    static IAudioManager s_service = new AudioManager();


    public static bool MusicOn
    {
        get
        {
            return s_service.MusicOn;
        }
        set
        {
            s_service.MusicOn = value;
        }

    }
    public static AudioSource PlaySound(AudioClip clip, Vector3 position)
    {
        return s_service.PlaySound(clip, position);
    }
    public static void PlayMusic(AudioClip aClip)
    {
        s_service.PlayMusic(aClip);
    }


    public static bool SoundOn
    {
        get
        {
            return s_service.SoundOn;
        }
        set
        {
            s_service.SoundOn = value;
        }
    }
}

public class AudioManager : IAudioManager
{
    bool m_soundOn;
    bool m_musicOn;
    AudioClip m_requestedMusicClip = null;
    List<AudioSource> m_activeSources = new List<AudioSource>();


    public AudioManager()
    {
        m_soundOn = true;
        m_musicOn = true;
    }
    public bool MusicOn
    {
        get
        {
            return m_musicOn;
        }
        set
        {
            m_musicOn = value;

            if (m_musicOn)
            {
                PlayMusic();
            }
            else
            {
                PauseMusic();
            }
        }
    }



    public bool SoundOn
    {
        get
        {
            return m_soundOn;
        }
        set
        {
            m_soundOn = value;
            float newVolume = m_soundOn ? 1 : 0;
            //deactivate/reactivate all sounds
            for (int i = 0; i < m_activeSources.Count; )
            {
                if (m_activeSources[i] == null)
                {
                    m_activeSources.RemoveAt(0);
                }
                else
                {
                    if (m_activeSources[i].isPlaying)
                    {
                        m_activeSources[i].volume = newVolume;
                    }
                    i++;
                }
            }
        }
    }


    public AudioSource PlaySound(AudioClip clip, Vector3 position)
    {
        if (m_soundOn)
        {
            GameObject go = new GameObject("Audio Source");
            AudioSource source = go.AddComponent<AudioSource>();

            go.transform.position = position;
            source.clip = clip;
            source.Play();
            MonoBehaviour.Destroy(go, clip.length);
            m_activeSources.Add(source);
            return source;
        }
        return null;
    }
    public void PlayMusic(AudioClip aClip)
    {
        m_requestedMusicClip = aClip;
        if (!m_musicOn)
        {
            return;
        }
        MusicPlayer.Instance.Play(aClip);
    }
    public void PlayMusic()
    {
        if (!m_musicOn)
        {
            return;
        }
        MusicPlayer.Instance.Play(m_requestedMusicClip);
    }
    public void PauseMusic()
    {
        MusicPlayer.Instance.Pause();
    }
}