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
    public AudioSource ButtonDodgeballMusic;
    public AudioSource TrailRumbleMusic;
    public AudioSource BulletButtonClick;
    public AudioSource Confetti;
    public AudioSource SideViewMusic;
    public AudioSource VicorySound;
    public AudioSource TrailSpeedUp;
    public AudioSource TrailSpeedUp2;

    float fade = 0.9f;
    float volume;
    int i = 0;

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

    public void playConfettiSound()
    {
        Confetti.Play();
    }


    public void PlayBulletButtonClick()
    {
        BulletButtonClick.Play();
    }


    public IEnumerator fadeTitleMusicOut()
    {
        volume = Title.volume;
        while(Title.volume > 0)
        {
            Title.volume -= 0.7f * Time.deltaTime / fade;
            yield return null;
            i++;
            if(i == 1000)
            {
                break;
            }
        }
        Title.Stop();
       
    }

    public IEnumerator fadeTitleMusicSoundIn()
    {

        //Debug.Log("Enter Title Music Fade In");
        i = 0;
        Title.volume = 0;
        volume = Title.volume;
        Title.Play();
        while (Title.volume < 1)
        {
            Title.volume += 0.7f * Time.deltaTime / fade;
            yield return null;
            i++;
            if (i == 2000)
            {
                break;
            }
        }

    }



    public IEnumerator fadeButtonDodgeballMusicIn()
    {
        
        //Debug.Log("In");
        i = 0;
        ButtonDodgeballMusic.volume = 0;
        volume = ButtonDodgeballMusic.volume;
        ButtonDodgeballMusic.Play();
        while (ButtonDodgeballMusic.volume < 1)
        {
            ButtonDodgeballMusic.volume += 0.7f * Time.deltaTime / fade;
            yield return null;
            i++;
            if (i == 1000)
            {
                break;
            }
        }

    }

    public IEnumerator fadeButtonDodgeballMusicOut()
    {
        volume = ButtonDodgeballMusic.volume;
        while (ButtonDodgeballMusic.volume > 0)
        {
            ButtonDodgeballMusic.volume -= 0.7f * Time.deltaTime / fade;
            yield return null;
            i++;
            if (i == 1000)
            {
                break;
            }
        }
    }

    public IEnumerator fadeTrailRumbleMusicIn()
    {

        //Debug.Log("In");
        i = 0;
        TrailRumbleMusic.volume = 0;
        volume = TrailRumbleMusic.volume;
        TrailRumbleMusic.Play();
        while (TrailRumbleMusic.volume < 1)
        {
            TrailRumbleMusic.volume += 0.7f * Time.deltaTime / fade;
            yield return null;
            i++;
            if (i == 1000)
            {
                break;
            }
        }

    }

    public IEnumerator fadeTrailRumbleMusicOut()
    {
        volume = TrailRumbleMusic.volume;
        while (TrailRumbleMusic.volume > 0)
        {
            TrailRumbleMusic.volume -= 0.7f * Time.deltaTime / fade;
            yield return null;
            i++;
            if (i == 1000)
            {
                break;
            }
        }
    }

    public IEnumerator fadeSideViewMusicIn()
    {

        //Debug.Log("In");
        i = 0;
        SideViewMusic.volume = 0;
        volume = SideViewMusic.volume;
        SideViewMusic.Play();
        while (SideViewMusic.volume < 1)
        {
            SideViewMusic.volume += 0.7f * Time.deltaTime / fade;
            yield return null;
            i++;
            if (i == 1000)
            {
                break;
            }
        }

    }

    public IEnumerator fadeSideViewMusicOut()
    {
        volume = SideViewMusic.volume;
        while (SideViewMusic.volume > 0)
        {
            SideViewMusic.volume -= 0.7f * Time.deltaTime / fade;
            yield return null;
            i++;
            if (i == 1000)
            {
                break;
            }
        }
    }


    public void StopAllGameMusic()
    {
        //GameMusic.Stop();
        //GameMusic2.Stop();
    }

    public void PlayVictorySound()
    {
        VicorySound.Play();
    }

    public void FadeGameMusic()
    {
        if(ButtonDodgeballMusic.isPlaying == true)
        {
            StartCoroutine(fadeButtonDodgeballMusicOut());
        }

        if(TrailRumbleMusic.isPlaying == true)
        {
            StartCoroutine(fadeTrailRumbleMusicOut());
        }

        if(SideViewMusic.isPlaying == true)
        {
            StartCoroutine(fadeSideViewMusicOut());
        }
    }

    public void playTrailSpeedUp()
    {
        TrailSpeedUp.Play();
    }

    public void stopTrailSpeedUp()
    {
        TrailSpeedUp.Stop();
    }

    public void playTrailSpeedUp2()
    {
        TrailSpeedUp2.Play();
    }

    public void stopTrailSpeedUp2()
    {
        TrailSpeedUp2.Stop();
    }

}
