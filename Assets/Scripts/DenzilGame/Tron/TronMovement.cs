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

    [SerializeField] HeartsKeeper heartsKeeper;
    [SerializeField] HeartsKeeperManager heartsKeeperManager;

    [SerializeField] Trail trail;

    void Start()
    {
        if (heartsKeeperManager.ResetHealths)
        {
            heartsKeeper.ResetHealth();
        }
        if (heartsKeeperManager.OtherPlayerReset)
        {
            heartsKeeperManager.ResetHealths = true;
            heartsKeeperManager.OtherPlayerReset = false;
        }
        else
        {
            heartsKeeperManager.OtherPlayerReset = true;
        }
        rig = GetComponent<Rigidbody2D>();
        SetupPlayerInput();
        heartScript = gameObject.GetComponent<Hearts>();
        heartScript.setHealth(heartsKeeper.health);
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
        float speed;
        if (Input.GetAxis(verticalInput) > 0)
        {
            speed = fasterSpeed;
        } else
        {
            speed = regularSpeed;
        }
        rig.MovePosition(rig.position + (Vector2)transform.up * speed * Time.fixedDeltaTime);
        transform.Rotate(0f, 0f, -Input.GetAxis(horizontalInput) * rotationSpeed, Space.Self);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Trail")
        {
            heartScript.subtractHealth();
            heartsKeeper.health--;
            if (heartScript.returnHealth() <= 0)
            {
                if (isPlayerOne)
                {
                    Debug.Log("Player Two Wins");
                }
                else
                {
                    Debug.Log("Player One Wins");
                }
                trail.StopAllCoroutines();
            }
            else
            {
                heartsKeeperManager.ResetHealths = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        } else if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Tie");
            heartsKeeperManager.ResetHealths = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
