using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPush : MonoBehaviour
{
    [SerializeField] private GameObject pushObject;
    private Animator pushAnimator;

    [SerializeField] private float pushDelay = 1.2f;
    private float pushDuration = 0.3f;

    [SerializeField] private int pushPower = 5;

    private bool canPush = true;

    private GameTimer pushTimer;
    private GameTimer pushCooldownTimer;

    private void Awake()
    {
        pushAnimator = pushObject.GetComponent<Animator>();

        pushTimer = new GameTimer(pushDuration, true);
        pushCooldownTimer = new GameTimer(pushDelay, true);

        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        if (newGameState == GameState.Gameplay)
        {
            enabled = true;
            pushAnimator.speed = 1;

            if (!canPush)
            {
                pushTimer.PauseTimer(false);

                if (pushTimer.GetTimerAlreadyFinished())
                {
                    pushCooldownTimer.PauseTimer(false);
                }
            }

        }
        else if (newGameState == GameState.Paused)
        {
            enabled = false;
            pushAnimator.speed = 0;

            pushTimer.PauseTimer(true);
            pushCooldownTimer.PauseTimer(true);
        }
    }

    public void ResetPushPower()
    {
        pushPower = 5;
    }

    public void Push()
    {
        if (canPush && Time.timeScale == 1)
        {
            canPush = false;
            pushAnimator.SetBool("IsPushing", true);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(pushObject.transform.position, 1);
            foreach (Collider2D collider in colliders)
            {
                if ((collider.tag == "Player1" || collider.tag == "Player2") && collider.gameObject != gameObject)
                {
                    collider.gameObject.GetComponent<PushMovement>().KnockBack(transform.up, pushPower);
                    pushPower = pushPower + 3;
                }
            }
            pushTimer.PauseTimer(false);
        }
    }

    private void Update()
    {
        if (pushTimer.UpdateTimer())
        {
            if (!pushTimer.GetTimerAlreadyFinished())
            {
                pushAnimator.SetBool("IsPushing", false);
                pushCooldownTimer.PauseTimer(false);
            }

            if (pushCooldownTimer.UpdateTimer())
            {
                canPush = true;
                pushTimer.RestartTimer();
                pushCooldownTimer.RestartTimer();
                pushTimer.PauseTimer(true);
                pushCooldownTimer.PauseTimer(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player1" || collision.tag == "Player2")
        {
            collision.GetComponent<PushMovement>().KnockBack(transform.up, pushPower);
            pushPower++;
        }
    }
}
