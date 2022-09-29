using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Inst = null;
    AudioSource _bgmSpeaker = null;
    public AudioSource bgmSpeaker
    {
        get
        {
            if(_bgmSpeaker == null)
            {
                _bgmSpeaker = Camera.main.GetComponent<AudioSource>();
            }
            return _bgmSpeaker;
        }
    }
    public float EffectVolume = 1.0f;
    private void Awake()
    {
        Inst = this;
    }
    
    public void PlayMusic(AudioClip clip, bool bLoop = true)
    {
        bgmSpeaker.clip = clip;
        bgmSpeaker.loop = bLoop;
        bgmSpeaker.Play();
    }

    public void PauseMusic(bool v)
    {
        if(v)
        {
            bgmSpeaker.Pause();
        }        
        else
        {
            bgmSpeaker.Play();
        }        
    }    
}
