using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiManager : Singleton<ConfettiManager>
{
    private Animator cannonAnimator;
    private GameTimer increaseScoreTimer;
    private GameTimer shrinkTextTimer;

    private bool redWon;

    private void Awake()
    {
        increaseScoreTimer = new GameTimer(3f, true);
        shrinkTextTimer = new GameTimer(3f, true);
        cannonAnimator = GetComponent<Animator>();
    }

    public void StartConfetti(bool redWon)
    {
        this.redWon = redWon;
        SoundManager.Instance.PlayVictorySound();
        SoundManager.Instance.FadeGameMusic();
        cannonAnimator.SetBool("Activated", true);
        increaseScoreTimer.PauseTimer(false);
    }

    public void OnlyPlayConfetti()
    {
        cannonAnimator.SetBool("Activated", true);
    }

    private void Update()
    {
        if (increaseScoreTimer.UpdateTimer())
        {
            if (!increaseScoreTimer.GetTimerAlreadyFinished())
            {
                GameUIManager.Instance.IncreaseScore(redWon);
                shrinkTextTimer.PauseTimer(false);
                StartCoroutine(Timer(1.2f));

            }

            if (shrinkTextTimer.UpdateTimer())
            {
                TransitionManager.Instance.PlayRandomEnterTransition();
                increaseScoreTimer.RestartTimer();
                shrinkTextTimer.RestartTimer();
                increaseScoreTimer.PauseTimer(true);
                shrinkTextTimer.PauseTimer(true);
            }
        }
    }

    IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        SoundManager.Instance.PlayIncreaseScoreSound();
    }
}
