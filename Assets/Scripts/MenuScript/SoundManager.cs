using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{

    public AudioSource Attack;
    public AudioSource Hit;
    public AudioSource Bounce;
    public AudioSource Death;
    public AudioSource Countdown;
    public AudioSource Shoot;
    public AudioSource Title;
    public AudioSource ButtonClick;
    public AudioSource GameMusic;
    public AudioSource GameMusic2;
    public AudioSource BulletButtonClick;

    //public AudioClip attackClip;
    //public AudioClip hitClip;
    // public AudioClip bounceClip;
    //public AudioClip deathClip;
    //public AudioClip countdownClip;
    // public AudioClip shootClip;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {







    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playAttackSound()
    {
        //  playAttack.clip = attackClip;
        Attack.Play();
    }

    public void playHitSound()
    {
        // playHit.clip = hitClip;
        Hit.Play();
    }

    public void playDeathSound()
    {
        // playDeath.clip = deathClip;
        Death.Play();
    }

    public void playCountdownSound()
    {
        // playCountdown.clip = countdownClip;
        Countdown.Play();
    }

    public void playShootSound()
    {
        // playShoot.clip = shootClip;
        Shoot.Play();
    }

    public void playBounceSound()
    {
        // playBounce.clip = bounceClip;
        Bounce.Play();
    }

    public void playTitleMusic()
    {
        Title.Play();
    }

    public void pauseTitleMusic()
    {
        Title.Stop();
    }

    public void playButtonClickSound()
    {
        ButtonClick.Play();
    }

    public void playGameMusicSound()
    {
        GameMusic.Play();
    }

    public void stopGameMusicSound()
    {
        GameMusic.Stop();
    }

    public void playGameMusicSound2()
    {
        GameMusic2.Play();
    }

    public void stopGameMusicSound2()
    {
        GameMusic2.Stop();
    }

    public void PlayBulletButtonClick()
    {
        BulletButtonClick.Play();
    }
}
