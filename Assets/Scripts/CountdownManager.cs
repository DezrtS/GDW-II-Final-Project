using UnityEngine;

public class CountdownManager : Singleton<CountdownManager>
{
    private Animator countdownAnimator;
    private GameTimer spawnCountdownTimer;
    private GameTimer countdownTimer;

    private bool isSpawning;
    private bool isCounting;
    private bool skipToCountdown;

    private void Awake()
    {
        countdownAnimator = GetComponent<Animator>();
        spawnCountdownTimer = new GameTimer(1f, true);
        countdownTimer = new GameTimer(3f, true);
    }

    private void Update()
    {
        if (spawnCountdownTimer.UpdateTimer() || skipToCountdown)
        {
            if (!spawnCountdownTimer.GetTimerAlreadyFinished() && !skipToCountdown)
            {
                isCounting = true;
                countdownAnimator.SetBool("IsCounting", isCounting);
                countdownTimer.PauseTimer(false);
            }

            if (countdownTimer.UpdateTimer())
            {
                GameStateManager.Instance.SetState(GameState.Gameplay);

                isCounting = false;
                isSpawning = false;
                countdownAnimator.SetBool("IsCounting", isCounting);
                countdownAnimator.SetBool("IsSpawning", isSpawning);

                spawnCountdownTimer.RestartTimer();
                countdownTimer.RestartTimer();
                spawnCountdownTimer.PauseTimer(true);
                countdownTimer.PauseTimer(true);

                skipToCountdown = false;
            }
        }
    }

    public void SpawnAndStartCountdown()
    {
        if (!isSpawning && !isCounting)
        {
            isSpawning = true;
            countdownAnimator.SetBool("IsSpawning", isSpawning);
            spawnCountdownTimer.PauseTimer(false);
        }
    }

    public void RestartCountdown()
    {
        if (!isSpawning && !isCounting)
        {
            isCounting = true;
            countdownAnimator.SetBool("IsCounting", true);
            countdownTimer.PauseTimer(false);
            skipToCountdown = true;
        }
    }
}
