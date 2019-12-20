using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Audiomanager : MonoBehaviour
{

    public static Audiomanager instance;
    public AudioMixer master;
    public AudioMixerGroup CharacterHurt;
    public AudioMixerGroup BossAtack;
    public AudioMixerGroup BossHurt;
    public AudioMixerGroup PistolShot;




    private void Awake()
    {
        instance = this;
    }

    public void CharacterDeath(AudioClip clip)
    {
        GameObject go = new GameObject("AudioSource");
        go.transform.parent = transform;        
        AudioSource source = go.AddComponent<AudioSource>();
        source.outputAudioMixerGroup = CharacterHurt;
        source.clip = clip;
        source.PlayOneShot(clip);
        Destroy(go, clip.length);
    }

  
    public void BossAtack1(AudioClip clip)
    {
        GameObject go = new GameObject("AudioSource");
        go.transform.parent = transform;
        AudioSource source = go.AddComponent<AudioSource>();
        source.outputAudioMixerGroup = BossAtack;
        source.clip = clip;
        source.PlayOneShot(clip);
        Destroy(go, clip.length);
    }

    public void BossHurt1(AudioClip clip)
    {
        GameObject go = new GameObject("AudioSource");
        go.transform.parent = transform;
        AudioSource source = go.AddComponent<AudioSource>();
        source.outputAudioMixerGroup = BossHurt;
        source.clip = clip;
        source.PlayOneShot(clip);
        Destroy(go, clip.length);
    }
    //public void PistolShot(AudioClip)
    //{
    //    GameObject go = new GameObject("AudioSource");
    //    go.transform.parent = transform;
    //    AudioSource source = go.AddComponent<AudioSource>();
    //    source.outputAudioMixerGroup = BossHurt;
    //    source.clip = clip;
    //    source.PlayOneShot(clip);
    //    Destroy(go, clip.length);

    //}



}
