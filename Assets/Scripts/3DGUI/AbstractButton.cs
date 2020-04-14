using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public abstract class AbstractButton : MonoBehaviour
{
    #region Fields Arranged Nicely :)
    public Camera m_camera;

    //public UnityEngine.Events.UnityEvent onButtonBegin;
    public UnityEngine.Events.UnityEvent onButtonRelease;

    public AudioClip m_clipOnBeginPress;
    public AudioClip m_clipOnEndPress;

    bool m_pressing = false;
    AudioSource m_playingAudioSource;
    #endregion Fields Arranged Nicely :)

    #region Methods

    public abstract bool TouchInsideButtonBounds(TouchGUI aTouch);

    void Update()
    {
        bool insideBounds = false;
        if (InputGUI.touches.Count > 0)
        {
            TouchGUI touch = InputGUI.touches[0];
            if (TouchInsideButtonBounds(touch))
            {
                insideBounds = true;
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        OnBeginPress();

                        break;
                    case TouchPhase.Moved:
                        break;

                    case TouchPhase.Ended:
                        if (m_pressing)
                        {
                            OnReleaseInBounds();
                        }

                        break;


                }

            }

        }
        if (!insideBounds)
        {
            if (m_pressing)
            {
                OnOutOfBounds();
            }
        }
    }

    protected virtual void Start()
    {


    }



    protected virtual void OnBeginPress()
    {
        //onButtonBegin.Invoke();
        m_pressing = true;
        if (m_clipOnBeginPress)
        {
            m_playingAudioSource = Audio.PlaySound(m_clipOnBeginPress, transform.position);
        }
    }

    protected virtual void OnReleaseInBounds()
    {


        onButtonRelease.Invoke();

        m_pressing = false;
        if (m_clipOnEndPress)
        {
            if (m_playingAudioSource)
            {
                Destroy(m_playingAudioSource.gameObject);
            }
            Audio.PlaySound(m_clipOnEndPress, transform.position);
        }
    }



    protected virtual void OnOutOfBounds()
    {

        m_pressing = false;
    }
    protected virtual void OnDisable()
    {
        m_pressing = false;
    }
    #endregion Methods
}