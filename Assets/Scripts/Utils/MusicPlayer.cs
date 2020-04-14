using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class MusicPlayer : SingletonMonoPrefab<MusicPlayer>
{
    #region Fields Arranged Nicely :)

    protected static new string Path()
    {
        return "MusicPlayer";
    }
    protected override void OnCreation()
    {
        
    }
    public AudioSource[] sources;

    public AudioSource currentSource;
    public float m_crossFadeTime = 1.5F;
    int currentSourceIndex = 0;
    #endregion Fields Arranged Nicely :)

    #region Methods
    IEnumerator Crossfade(AudioClip aNewClip)
    {
        AudioSource emergingAudioSource = sources[++currentSourceIndex % sources.Length];
        AudioSource fadingAudioSource = currentSource;
        currentSource = emergingAudioSource;
        emergingAudioSource.time = 0;
        emergingAudioSource.volume = 0;
        emergingAudioSource.clip = aNewClip;
        emergingAudioSource.Stop();
        emergingAudioSource.Play();
        float startTime = Time.time;
        float endTime = startTime + m_crossFadeTime;

        while (Time.time < endTime)
        {

            float i = (Time.time - startTime) / m_crossFadeTime;

            fadingAudioSource.volume = Mathf.Lerp(1, 0, i);
            emergingAudioSource.volume = Mathf.Lerp(0, 1, i);

            yield return null;

        }
        emergingAudioSource.volume = 1;
        fadingAudioSource.Stop();
        fadingAudioSource.volume = 0;
    }
    public void Play(AudioClip aClip)
    {
        if (currentSource.clip != aClip)
        {
            //crossfade to new clip change clip
            StopAllCoroutines();
            StartCoroutine(Crossfade(aClip));
        }
        else
        {
            currentSource.Play();
        }
        
    }
    
    public void Pause()
    {
        //Debug.Log("must pause source " + currentSource.name);
        currentSource.Pause();
    }
  

    #endregion Methods
}