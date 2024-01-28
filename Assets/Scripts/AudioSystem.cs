using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    //public
    static public AudioSystem instance;
    //private
    private AudioSource _backGroundPlayer, _soundEffectPlayer;
    private AudioClip _backGroundMusic;
    private Dictionary<string,AudioClip> _soundEffectMusic = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        _backGroundMusic = Resources.Load<AudioClip>("Audio/BackGround");
        
        var data = Resources.LoadAll<AudioClip>("Audio/SoundEffect");
        foreach (var value in data)
        {
            _soundEffectMusic.Add(value.name,value);
        }
        
        _backGroundPlayer = transform.Find("BackGround").GetComponent<AudioSource>();
        _soundEffectPlayer = transform.Find("SoundEffect").GetComponent<AudioSource>();

        _backGroundPlayer.loop = true;
        _backGroundPlayer.playOnAwake = false;

        _soundEffectPlayer.playOnAwake = false;
    }

    private void Start()
    {
        if(instance == null)
            instance = this;
        
        

        
        PlayBackGroundMusic();  
    }

    private void PlayBackGroundMusic()
    {
        _backGroundPlayer.clip = _backGroundMusic;
        _backGroundPlayer.Play();
    }

    public void PlaySoundEffect(string key)
    {
        if(!_soundEffectMusic.ContainsKey(key)) return;
        _soundEffectPlayer.PlayOneShot(_soundEffectMusic[key]);
        print(key);
    }
}
