using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Set Player Values
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
        //Call screen size
        screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

    private void OnBecameInvisible()
    {
        //Allow player to return back to the other side of the screen if they leave the camera
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
        //Set walking inputs
        rig.velocity = new Vector2(0, rig.velocity.y);

        float movementInput;
        float jumpInput;

        if (isPlayerOne)
        {
            movementInput = Input.GetAxis("Horizontal");
            jumpInput = Input.GetAxis("Vertical");

            if (movementInput == 0 && grounded)
            {
                //Set animation to idle
                anim.SetBool("isRunning", false);
            }
            else
            {
                //Set animation to running
                anim.SetBool("isRunning", true);
            }

            if (movementInput > 0)
            {
                // Flip the sprite to face right
                transform.localScale = new Vector3(2, 2, 2); 
            }
            else if (movementInput < 0)
            {
                // Flip the sprite to face left
                transform.localScale = new Vector3(-2, 2, 2); 
            }

        }
        else
        {
            movementInput = Input.GetAxis("Horizontal2");
            jumpInput = Input.GetAxis("Vertical2");

            if (movementInput == 0 && grounded)
            {
                //Set player 2 animation to idle
                anim.SetBool("isRunning2", false);
            }
            else
            {
                //Set player 2 animation to running
                anim.SetBool("isRunning2", true);
            }

            if (movementInput > 0)
            {
                // Flip the sprite to face right
                transform.localScale = new Vector3(2, 2, 2);
            }
            else if (movementInput < 0)
            {
                // Flip the sprite to face left
                transform.localScale = new Vector3(-2, 2, 2); 
            }

        }

        //Set jumping inputs
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

            //Allow player to jump
            rig.velocity = new Vector2(rig.velocity.x, 0);
            rig.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
        }
        else
        {
            
        }

        //Set jumping animations
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

        //Allow player to move
        rig.velocity = new Vector2(movementInput * speed, rig.velocity.y);
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
        //Subtract health from the player if projectile collides with player
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

        //Check if game ended and who won
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
            GameEnder.Instance.StartEndGame(!isPlayerOne);
        }
    }

    //Get player 1 health
    public int ReturnP1Health()
    {
        return (heartScript.returnHealth());
    }

    //Get player 2 health
    public int ReturnP2Health()
    {
        return (heartScript.returnHealth());
    }
}
