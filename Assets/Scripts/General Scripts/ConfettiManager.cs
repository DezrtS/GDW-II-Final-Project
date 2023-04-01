using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiManager : Singleton<ConfettiManager>
{
    private Animator cannonAnimator;
    private GameTimer fireConfettiTimer;

    private void Awake()
    {
        fireConfettiTimer = new GameTimer(0.5f, true);
        cannonAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartConfetti();
    }

    public void StartConfetti()
    {
        SoundManager.Instance.PlayVictorySound();
        cannonAnimator.SetBool("Activated", true);
        fireConfettiTimer.PauseTimer(false);


    }

    private void Update()
    {
        if (fireConfettiTimer.UpdateTimer())
        {
            SoundManager.Instance.FadeGameMusic();
            fireConfettiTimer.SetTimeTillCompletion(1.5f);
            fireConfettiTimer.RestartTimer();
            fireConfettiTimer.PauseTimer(true);
        }
    }
}
