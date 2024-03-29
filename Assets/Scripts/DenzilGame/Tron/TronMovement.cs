using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TronMovement : MonoBehaviour
{
    public bool isPlayerOne;
    string horizontal1 = "Horizontal", horizontal2 = "Horizontal2", vertical1 = "Vertical", vertical2 = "Vertical2", button1 = "Fire1", button2 = "Fire2";
    string horizontalInput, verticalInput, fireInput;

    Rigidbody2D rig;

    [SerializeField] float regularSpeed = 5;
    [SerializeField] float fasterSpeed = 10;
    [SerializeField] float rotationSpeed = 5;

    [SerializeField] Hearts heartScript;

    [SerializeField] public HeartsKeeper heartsKeeper;

    [SerializeField] public Trail trail;

    public bool canMove = true;

    void Start()
    {
        if (heartsKeeper.resetHealths)
        {
            heartsKeeper.ResetHealth();
        }
        if (heartsKeeper.otherPlayerReset)
        {
            heartsKeeper.resetHealths = true;
            heartsKeeper.otherPlayerReset = false;
        } else
        {
            heartsKeeper.otherPlayerReset = true;
        }
        rig = GetComponent<Rigidbody2D>();
        SetupPlayerInput();
        heartScript = gameObject.GetComponent<Hearts>();
        heartScript.setHealth(heartsKeeper.GetHealth(isPlayerOne));
    }

    void Update()
    {
         
    }

    private void FixedUpdate()
    {
        Move();
    }

    void SetupPlayerInput()
    {
        if (isPlayerOne)
        {
            horizontalInput = horizontal1;
            verticalInput = vertical1;
            fireInput = button1;
        }
        else
        {
            horizontalInput = horizontal2;
            verticalInput = vertical2;
            fireInput = button2;
        }
    }

    void Move()
    {
        if (canMove)
        {
            float speed;
            if (Input.GetAxis(verticalInput) > 0)
            {
                speed = fasterSpeed;
            }
            else
            {
                speed = regularSpeed;
            }
            rig.MovePosition(rig.position + (Vector2)transform.up * speed * Time.fixedDeltaTime);
            transform.Rotate(0f, 0f, -Input.GetAxis(horizontalInput) * rotationSpeed, Space.Self);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Tie");
            heartsKeeper.canTakeAwayHealth = false;
            //heartsKeeper.resetHealths = false;
            trail.StopAllCoroutines();
            TrailGameController.instance.FreezeGame();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        } else if (collision.gameObject.tag == "Trail")
        {
            if (heartsKeeper.BothPlayersAlive() && heartsKeeper.canTakeAwayHealth)
            {
                heartScript.subtractHealth();
                heartsKeeper.TakeAwayHealth(isPlayerOne);
            
            }

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
                TrailGameController.instance.FreezeGame(true);
            }
            else if (heartsKeeper.BothPlayersAlive())
            {
                //heartsKeeper.resetHealths = false;
                trail.StopAllCoroutines();
                TrailGameController.instance.FreezeGame();
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            heartsKeeper.canTakeAwayHealth = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Shortener")
        {
            TrailGameController.instance.ShortenOtherPlayerTail(isPlayerOne);
            Destroy(collision.gameObject);
        }
    }
}
