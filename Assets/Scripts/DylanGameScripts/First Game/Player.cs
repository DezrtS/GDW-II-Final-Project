using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rig;
    [SerializeField] private float speed = 1;
    [SerializeField] private float jumpPower = 1;
    [SerializeField] private bool isPlayerOne;
    private bool grounded;
    private Vector3 groundNormal = Vector3.right;
    private float screenWidth;
    public Vector3 startingPosition;

    private Animator anim;

    private Hearts heartScript;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        heartScript = gameObject.GetComponent<Hearts>();

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
            anim.speed = 1;
        }
        else if (newGameState == GameState.Paused)
        {
            enabled = false;
            rig.simulated = false;
            anim.speed = 0;
        }
    }

    private void Start()
    {
        screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

    

    private void OnBecameInvisible()
    {
        if (transform.position.x > screenWidth)
        {
            transform.position = new Vector3(-screenWidth, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -screenWidth)
        {
            transform.position = new Vector3(screenWidth, transform.position.y, transform.position.z);
        }
    }

    private void FixedUpdate()
    {
        rig.velocity = new Vector2(0, rig.velocity.y);

        float movementInput;
        float jumpInput;

        if (isPlayerOne)
        {
            movementInput = Input.GetAxis("Horizontal");
            jumpInput = Input.GetAxis("Vertical");

            if (movementInput == 0 && grounded)
            {
                anim.SetBool("isRunning", false);
            }
            else
            {
                anim.SetBool("isRunning", true);
            }

            if (movementInput > 0)
            {
                transform.localScale = new Vector3(2, 2, 2); // Flip the sprite to face right
            }
            else if (movementInput < 0)
            {
                transform.localScale = new Vector3(-2, 2, 2); // Flip the sprite to face left
            }

        }
        else
        {
            movementInput = Input.GetAxis("Horizontal2");
            jumpInput = Input.GetAxis("Vertical2");

            if (movementInput == 0 && grounded)
            {
                anim.SetBool("isRunning2", false);
            }
            else
            {
                anim.SetBool("isRunning2", true);
            }

            if (movementInput > 0)
            {
                transform.localScale = new Vector3(2, 2, 2); // Flip the sprite to face right
            }
            else if (movementInput < 0)
            {
                transform.localScale = new Vector3(-2, 2, 2); // Flip the sprite to face left
            }

        }

        if (grounded && jumpInput > 0)
        {
            if (isPlayerOne)
            {
                anim.SetTrigger("takeOff");
            }
            else
            {
                anim.SetTrigger("takeOff2");
            }
            //anim.SetTrigger("Player1Jump");
            rig.velocity = new Vector2(rig.velocity.x, 0);
            rig.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
            // anim.SetBool("isJumping", false);
        }
        else
        {
            //anim.SetBool("isJumping", true);
        }

        if (grounded == true)
        {
            if (isPlayerOne)
            {
                anim.SetBool("isJumping", false);
            }
            else
            {
                anim.SetBool("isJumping2", false);
            }
        }
        else
        {
            if (isPlayerOne)
            {
                anim.SetBool("isJumping", true);
            }
            else
            {
                anim.SetBool("isJumping2", true);
            }
        }
        //rig.velocity = new Vector2(movementInput * speed, rig.velocity.y);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + movementInput * groundNormal, Time.deltaTime * speed);
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
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        groundNormal = Vector3.right;
        grounded = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("Player1") || gameObject.CompareTag("Player2"))
        {
            if (collision.CompareTag("ProjectilePlayer1") || collision.CompareTag("ProjectilePlayer2"))
            {
                if ((isPlayerOne && collision.CompareTag("ProjectilePlayer2")) ||
                    (!isPlayerOne && collision.CompareTag("ProjectilePlayer1")))
                {
                    heartScript.subtractHealth();
                }
            }
        }

        if (heartScript.returnHealth() <= 0 && !GameEnder.Instance.IsGameEnding())
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
    }

    public int ReturnP1Health()
    {
        return (heartScript.returnHealth());
    }

    public int ReturnP2Health()
    {
        return (heartScript.returnHealth());
    }
}
