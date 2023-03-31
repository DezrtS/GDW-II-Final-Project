using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailMovement : MonoBehaviour
{
    private string horizontal1 = "Horizontal", horizontal2 = "Horizontal2", vertical1 = "Vertical", vertical2 = "Vertical2", button1 = "Fire1", button2 = "Fire2";
    private string horizontalInputString, verticalInputString, actionInputString;

    private Rigidbody2D rig;

    [SerializeField] private float regularSpeed = 5;
    [SerializeField] private float fasterSpeed = 10;
    [SerializeField] private float rotationSpeed = 5;

    [SerializeField] public HeartsKeeper heartsKeeper;

    [SerializeField] public Trail trail;

    private Hearts heartScript;

    public bool canMove = true;
    public bool isPlayerOne;

    private bool canDropTail = true;

    private float horizontalInput;
    private float verticalInput;

    private GameTimer tailDropTimer;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        heartScript = gameObject.GetComponent<Hearts>();
        SetupPlayerInput();

        tailDropTimer = new GameTimer(1f, true);

        if (heartsKeeper.resetHealths)
        {
            heartsKeeper.ResetHealth();
        }
        if (heartsKeeper.otherPlayerReset)
        {
            heartsKeeper.resetHealths = true;
            heartsKeeper.otherPlayerReset = false;
        }
        else
        {
            heartsKeeper.otherPlayerReset = true;
        }
        heartScript.setHealth(heartsKeeper.GetHealth(isPlayerOne));

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
            rig.simulated = true;
            if (!canDropTail)
            {
                tailDropTimer.PauseTimer(false);
            }
        }
        else if (newGameState == GameState.Paused)
        {
            enabled = false;
            rig.simulated = false;
            tailDropTimer.PauseTimer(true);
        }
    }

    private void SetupPlayerInput()
    {
        if (isPlayerOne)
        {
            horizontalInputString = horizontal1;
            verticalInputString = vertical1;
            actionInputString = button1;
        }
        else
        {
            horizontalInputString = horizontal2;
            verticalInputString = vertical2;
            actionInputString = button2;
        }
    }

    private void Update()
    {
        verticalInput = Input.GetAxis(verticalInputString);
        horizontalInput = Input.GetAxis(horizontalInputString);

        if (Input.GetButtonDown(actionInputString) && canDropTail && Time.timeScale == 1)
        {
            trail.PlaceTrail(isPlayerOne);
            trail.ShrinkTailNow(2);
            canDropTail = false;
            tailDropTimer.PauseTimer(false);
        }

        if (tailDropTimer.UpdateTimer())
        {
            canDropTail = true;
            tailDropTimer.RestartTimer();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float speed;
        if (verticalInput > 0)
        {
            speed = fasterSpeed;
        }
        else
        {
            speed = regularSpeed;
        }
        rig.MovePosition(rig.position + (Vector2)transform.up * speed * Time.fixedDeltaTime);
        transform.Rotate(0f, 0f, -horizontalInput * rotationSpeed, Space.Self);
    }

    public void FreezePlayer()
    {
        OnGameStateChanged(GameState.Paused);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Player" || collision.gameObject.tag == "Trail") && heartsKeeper.canTakeAwayHealth)
        {
            heartsKeeper.canTakeAwayHealth = false;

            heartScript.subtractHealth();
            heartsKeeper.TakeAwayHealth(isPlayerOne);
            ShakeBehaviour.Instance.TriggerShake();

            if (heartScript.returnHealth() <= 0)
            {
                SoundManager.Instance.playDeathSound();
                if (isPlayerOne)
                {
                    Debug.Log("Player Two Wins");
                    P2Score.Instance.AddScore();
                }
                else
                {
                    Debug.Log("Player One Wins");
                    P1Score.Instance.AddScore();
                }
                GameEnder.Instance.StartEndGame();
                TrailGameController.Instance.FreezePlayers();
            }
            else
            {
                SoundManager.Instance.playHitSound();
                trail.StopAllCoroutines();
                TrailGameController.Instance.ResetGame();
            }
        }
    }
}
