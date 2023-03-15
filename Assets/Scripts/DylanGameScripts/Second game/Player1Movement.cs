using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Movement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 4;
    [SerializeField] private float minBouncePower = 1f;
    [SerializeField] private float maxBouncePower = 5f;
    [SerializeField] private float bouncePowerIncrement = 0.5f;
    [SerializeField] private float respawnTime = 2f;

    public Rigidbody2D body;

    public Camera gameCamera;

    [SerializeField] private bool isPlayerOne;

    private float bouncePower = 0f;
    private bool isRespawning = false;
    private Vector2 respawnPosition;

    Vector2 move;

    public void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        respawnPosition = gameCamera.transform.position;
    }

    // Update is called once per frame
    public void Update()
    {
        if (!isRespawning)
        {
            if (isPlayerOne)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    move.x = -1;
                }

                else if (Input.GetKey(KeyCode.D))
                {
                    move.x = 1;
                }

                else
                {
                    move.x = 0;
                }

                if (Input.GetKey(KeyCode.W))
                {
                    move.y = 1;
                }

                else if (Input.GetKey(KeyCode.S))
                {
                    move.y = -1;
                }

                else
                {
                    move.y = 0;
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    move.x = -1;
                }

                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    move.x = 1;
                }

                else
                {
                    move.x = 0;
                }

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    move.y = 1;
                }

                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    move.y = -1;
                }

                else
                {
                    move.y = 0;
                }
            }
        }
    }

    public void FixedUpdate()
    {
        if (!isRespawning)
        {
            body.MovePosition(body.position + move * playerSpeed * Time.fixedDeltaTime);
        }
        else
        {
            body.MovePosition(Vector2.Lerp(body.position, respawnPosition, Time.fixedDeltaTime / respawnTime));
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            Debug.Log("What's up");

            if (collision.contacts.Length > 0)
            {
                int numCollisions = collision.contacts.Length;
                bouncePower = Mathf.Clamp(bouncePower + numCollisions * bouncePowerIncrement, minBouncePower, maxBouncePower);

                Vector2 direction = (body.position - collision.rigidbody.position).normalized;
                body.velocity = direction * bouncePower;
                collision.rigidbody.velocity = -direction * bouncePower;
            }
            else
            {
                Debug.Log("Hello your game sucks");
            }
        }
        else if (collision.gameObject.CompareTag("Boundary"))
        {
            isRespawning = true;
            body.velocity = Vector2.zero;
            transform.position = Vector3.zero;
            Invoke("ResetRespawn", 1f); 
        }
        else if (collision.gameObject.CompareTag("BounceObject"))
        {
            // Bounce off prefab
            if (collision.contacts.Length > 0)
            {
                int numCollisions = collision.contacts.Length;
                bouncePower = Mathf.Clamp(bouncePower + numCollisions * bouncePowerIncrement, minBouncePower, maxBouncePower);

                Vector2 direction = (body.position - collision.rigidbody.position).normalized;
                body.velocity = direction * bouncePower;
                collision.rigidbody.velocity = -direction * bouncePower;
            }
        }
    }

    void ResetRespawn()
    {
        isRespawning = false;
        bouncePower = minBouncePower;
    }
}