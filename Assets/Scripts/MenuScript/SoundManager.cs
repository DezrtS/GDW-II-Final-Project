using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{

    public AudioSource playAttack;
    public AudioSource playHit;
    public AudioSource playBounce;
    public AudioSource playDeath;
    public AudioSource playCountdown;
    public AudioSource playShoot;
    

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
        playAttack.Play();
    }

    public void playHitSound()
    {
       // playHit.clip = hitClip;
        playHit.Play();
    }

    public void playDeathSound()
    {
       // playDeath.clip = deathClip;
        playDeath.Play();
    }

    public void playCountdownSound()
    {
       // playCountdown.clip = countdownClip;
        playCountdown.Play();
    }

    public void playShootSound()
    {
       // playShoot.clip = shootClip;
        playShoot.Play();
    }

    public void playBounceSound()
    {
       // playBounce.clip = bounceClip;
        playBounce.Play();
    }

}
