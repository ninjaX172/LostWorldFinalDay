
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{

    private AudioSource _musicSource;
    private AudioSource _sfxSource;
    
    public AudioClip backgroundMusic;
    public List<AudioClip> bossMusics;

    
    public AudioClip lastStandMusic;
    public AudioClip grimStunnedSound;
    public AudioClip grimUnstunnedSound;
    
    public AudioClip churchBellSound;
    public AudioClip portalEnteredSound;
    public AudioClip selectSound;
    
    void Start()
    {
        _musicSource = transform.Find("Music Audio Source").GetComponent<AudioSource>();
        _sfxSource = transform.Find("SFX Audio Source").GetComponent<AudioSource>();
        
        _musicSource.loop = true;
        _sfxSource.loop = false;
        PlayBackgroundMusic();
    }

    void Update()
    {
        
    }

    void OnEnable()
    {
        EventManager.Instance.GrimStunned += EventManager_OnGrimStunned;
        EventManager.Instance.GrimUnstunned += EventManager_OnGrimUnstunned;
        EventManager.Instance.PortalEntered += EventManager_OnPortalEntered;
        EventManager.Instance.BossDeath += EventManager_OnBossDeath;
        EventManager.Instance.PlayerEnteredGrimTrial += EventManager_OnPlayerEnteredGrimTrial;
        EventManager.Instance.PlayerDefeatedAllBosses += EventManager_OnPlayerDefeatedAllBosses;
        EventManager.Instance.PerkSelected += EventManager_OnPerkSelected;
    }
    void OnDisable()
    {
        EventManager.Instance.GrimStunned -= EventManager_OnGrimStunned;
        EventManager.Instance.GrimUnstunned -= EventManager_OnGrimUnstunned;
        EventManager.Instance.PortalEntered -= EventManager_OnPortalEntered;
        EventManager.Instance.BossDeath -= EventManager_OnBossDeath;
        EventManager.Instance.PlayerEnteredGrimTrial -= EventManager_OnPlayerEnteredGrimTrial;
        EventManager.Instance.PlayerDefeatedAllBosses -= EventManager_OnPlayerDefeatedAllBosses;
        EventManager.Instance.PerkSelected -= EventManager_OnPerkSelected;
    }



    private void EventManager_OnPerkSelected(BasePerk arg0)
    {
        _sfxSource.clip = selectSound;
        _sfxSource.Play();
    }

    private void EventManager_OnPlayerDefeatedAllBosses()
    {
        PlayChurchBell(13);
        StartCoroutine(StartTransition(lastStandMusic, 5f));
    }

    private void EventManager_OnPlayerEnteredGrimTrial()
    {
        _musicSource.clip = lastStandMusic;
        _musicSource.Play();
    }

    private void EventManager_OnBossDeath(string arg0, Vector2Int arg1)
    {
        StartCoroutine(StartTransition(backgroundMusic, 5f));
        PlayChurchBell(3);
        
    }

    private void EventManager_OnPortalEntered()
    {
        _sfxSource.clip = portalEnteredSound;
        _sfxSource.Play();
        
        var bossMusic = bossMusics[UnityEngine.Random.Range(0, bossMusics.Count)];
        StartCoroutine(StartTransition(bossMusic, 3f));
        
    }

    private void EventManager_OnGrimUnstunned()
    {
        throw new NotImplementedException();
    }

    private void EventManager_OnGrimStunned()
    {
        _sfxSource.clip = grimStunnedSound;
        _sfxSource.Play();
    }

    private IEnumerator  StartTransition(AudioClip toSound, float transitionTime)
    {
        float timer = 0f;
        float startVolume = _musicSource.volume;

        while (timer < transitionTime)
        {
            _musicSource.volume = Mathf.Lerp(startVolume, 0f, timer / transitionTime);
            timer += Time.deltaTime;
            yield return null;
        }

        _musicSource.volume = 0;
        _musicSource.clip = toSound;
        _musicSource.Play();

        timer = 0f;
        while (timer < transitionTime)
        {
            _musicSource.volume = Mathf.Lerp(0f, startVolume, timer / transitionTime);
            timer += Time.deltaTime;
            yield return null;
        }
        
        _musicSource.volume = startVolume;
    }

    void PlayBackgroundMusic()
    {
        _musicSource.clip = backgroundMusic;
        _musicSource.Play();
    }
    
    
    
    void PlayChurchBell(float duration)
    {
        _sfxSource.clip = churchBellSound;
        _sfxSource.SetScheduledEndTime(AudioSettings.dspTime + duration);
        _sfxSource.Play();
    }
    
    
}