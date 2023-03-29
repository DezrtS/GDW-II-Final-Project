using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PivotPlayer : MonoBehaviour
{
    private string horizontal1 = "Horizontal", horizontal2 = "Horizontal2", vertical1 = "Vertical", vertical2 = "Vertical2", button1 = "Fire1", button2 = "Fire2";
    private string horizontalInputString, verticalInputString, actionInputString;

    private Rigidbody2D rig;
    [SerializeField] private Weapon playerWeapon;
    [SerializeField] private float speed = 1;
    [SerializeField] private float jumpPower = 1;
    [SerializeField] private bool isPlayerOne;
    [SerializeField] private HeartsKeeper heartsKeeper;
    [SerializeField] private ParticleSystem lavaSplash;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animator slashAnimator;

    private Hearts heartScript;

    private bool grounded;
    private Vector3 groundNormal = Vector3.right;
    private Vector3 normal = Vector3.right;
    private Vector3 knockback;

    private bool isWalking = false;
    private bool keepRotation = false;

    private float horizontalInput;
    private float verticalInput;

    private bool isAttacking = false;
    private float attackDirection = 1;

    private GameTimer playerTimer;
    private GameTimer slashTimer;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        heartScript = gameObject.GetComponent<Hearts>();
        SetupPlayerInput();

        playerTimer = new GameTimer(playerWeapon.timeTillAttack / 2f, true);
        slashTimer = new GameTimer(playerWeapon.timeTillAttack / 2f, true);

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

            if (lavaSplash.isPaused)
            {
                lavaSplash.Play();
            }

            if (isAttacking)
            {
                playerTimer.PauseTimer(false);
                if (playerTimer.GetTimerAlreadyFinished())
                {
                    slashTimer.PauseTimer(false);
                }
            }

            playerAnimator.speed = 1;
            slashAnimator.speed = 1;
        }
        else if (newGameState == GameState.Paused)
        {
            enabled = false;
            rig.simulated = false;

            if (lavaSplash.isPlaying)
            {
                lavaSplash.Pause();
            }

            playerTimer.PauseTimer(true);
            slashTimer.PauseTimer(true);

            playerAnimator.speed = 0;
            slashAnimator.speed = 0;
        }
    }

    void SetupPlayerInput()
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
        
        if (Input.GetButtonDown(actionInputString) && !isAttacking && Time.timeScale == 1)
        {
            isAttacking = true;
            playerAnimator.SetBool("IsAttacking", true);

            attackDirection = Mathf.Sign((PivotGameController.Instance.GetClosestPlayer(gameObject).transform.position - transform.position).x);
            keepRotation = true;
            if (attackDirection > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }

            playerTimer.PauseTimer(false);
        }

        if (playerTimer.UpdateTimer())
        {
            if (!playerTimer.GetTimerAlreadyFinished())
            {
                SoundManager.Instance.playAttackSound();
                slashAnimator.SetBool("IsAttacking", true);
                //slashTimer.RestartTimer();
                slashTimer.PauseTimer(false);
            }

            if (slashTimer.UpdateTimer())
            {
                isAttacking = false;
                playerAnimator.SetBool("IsAttacking", false);
                slashAnimator.SetBool("IsAttacking", false);

                keepRotation = false;

                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + Vector3.right * attackDirection * playerWeapon.attackRange, playerWeapon.attackRadius);
                foreach (Collider2D collider in colliders)
                {
                    if (collider.gameObject != gameObject && collider.gameObject.tag == "Player")
                    {
                        collider.gameObject.GetComponent<PivotPlayer>().ApplyKnockback(new Vector3(playerWeapon.knockbackAmount * attackDirection, 0, 0));
                        SoundManager.Instance.playHitSound();
                    }
                }

                playerTimer.RestartTimer();
                slashTimer.RestartTimer();
                playerTimer.PauseTimer(true);
                slashTimer.PauseTimer(true);
            }
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        knockback = knockback * Mathf.Lerp(1, 0, Time.deltaTime * 1.5f);

        if (Mathf.Abs(knockback.x) > 1f)
        {
            horizontalInput = 0;
        }

        if (horizontalInput != 0 && grounded)
        {
            if (!isWalking)
            {
                isWalking = true;
                playerAnimator.SetBool("IsWalking", isWalking);
            }
        }
        else
        {
            if (isWalking)
            {
                isWalking = false;
                playerAnimator.SetBool("IsWalking", isWalking);
            }
        }
        if (!keepRotation)
        {
            if (horizontalInput > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (horizontalInput < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }

        Vector3 horizontalVelocity = (horizontalInput * speed * Time.fixedDeltaTime * groundNormal) + (knockback);

        if (grounded && verticalInput > 0)
        {
            rig.velocity = new Vector2(rig.velocity.x, 0);
            rig.AddForce(jumpPower * Vector2.up, ForceMode2D.Impulse);
            grounded = false;
        }

        if (grounded)
        {
            rig.velocity = horizontalVelocity + new Vector3(0, -0.5f, 0);
        }
        else
        {
            if (Mathf.Abs(rig.velocity.x) > 1)
            {
                rig.velocity = new Vector2(horizontalVelocity.x, rig.velocity.y);
            }
            else
            {
                rig.AddForce(new Vector2(horizontalInput * normal.x, 0), ForceMode2D.Impulse);
            }
        }
    }

    public void ApplyKnockback(Vector3 knockback)
    {
        rig.AddForce(knockback);
        this.knockback = this.knockback + knockback;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Vector2 groundContactNormal = Vector2.right;
        for (int i = 0; i < collision.contacts.Length; i++)
        {
            if (collision.contacts[i].normal.y > 0.5f)
            {
                groundContactNormal = collision.contacts[i].normal;
                break;
            }
        }

        groundNormal = Quaternion.Euler(0, 0, -90) * groundContactNormal;
        if (groundContactNormal.y < 0.5f)
        {
            groundNormal = Vector3.right;
        }
        else
        {
            grounded = true;
        }
        normal = groundNormal;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
        groundNormal = Vector3.right;
        grounded = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Lava")
        {
            SoundManager.Instance.playDeathSound();
            ShakeBehaviour.instance.TriggerShake();
            lavaSplash.Play();
            rig.drag = 25;
            gameObject.layer = 12;
            if (!heartsKeeper.otherPlayerTakenDamage)
            {
                heartScript.subtractHealth();
                heartsKeeper.TakeAwayHealth(isPlayerOne);
                heartsKeeper.otherPlayerTakenDamage = true;
            }
            else
            {
                heartsKeeper.otherPlayerTakenDamage = false;
            }

        }
        else if (collision.gameObject.tag == "Reset")
        {
            if (heartScript.returnHealth() <= 0)
            {
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
            }
            else
            {
                if (heartsKeeper.BothPlayersAlive())
                {
                    heartsKeeper.resetHealths = false;
                    heartsKeeper.otherPlayerTakenDamage = false;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }
    }

}
