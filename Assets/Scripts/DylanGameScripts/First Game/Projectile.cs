using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    private float screenWidth;
    private Vector3 originalVelocity;

    public float speed1 = 10f;
    public float lifeTime = 2f;

    private Rigidbody2D rb;

    private float startingTime;

    int player1Health = 3;
    int player2Health = 3;

    [SerializeField] private bool isPlayerOne;

    [SerializeField] Hearts heartScript;

    private void Start()
    {
        screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        originalVelocity = GetComponent<Rigidbody2D>().velocity;
        rb = GetComponent<Rigidbody2D>();

        if (isPlayerOne)
        {
            if (GameObject.Find("Player1").GetComponent<SpriteRenderer>().flipX)
            {
                rb.velocity = -transform.right * speed1;
            }
            else
            {
                rb.velocity = transform.right * speed1;
            }
        }
        else
        {
            if (GameObject.Find("Player2").GetComponent<SpriteRenderer>().flipX)
            {
                rb.velocity = -transform.right * speed1;
            }
            else
            {
                rb.velocity = transform.right * speed1;
            }
        }
        heartScript = gameObject.GetComponent<Hearts>();
        //Destroy(gameObject, lifeTime);
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
        else if (transform.position.y > Camera.main.orthographicSize)
        {
            transform.position = new Vector3(transform.position.x, -Camera.main.orthographicSize, transform.position.z);
        }
        else if (transform.position.y < -Camera.main.orthographicSize)
        {
            transform.position = new Vector3(transform.position.x, Camera.main.orthographicSize, transform.position.z);
        }
    }


    private void Update()
    {
        Vector3 direction = Vector3.zero;

        //if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.R))
        //{
        //    direction += transform.right;
        //}
        //if (Input.GetKey(KeyCode.R))
        //{
        //    direction += transform.right;
        //}
        /*if (Input.GetKey(KeyCode.R))
        {
            direction += Vector3.up;
        }*/
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("ProjectilePlayer1"))
        {
            if (collision.CompareTag("Player2"))
            {
                PlayerScoreManager.UpdatePlayerScore("Player2");
                Destroy(gameObject);
                SoundManager.Instance.playDeathSound();

                if (!isPlayerOne)
                {
                    heartScript.subtractHealth();
                }

                // Destroy all other projectiles in the scene
                Projectile[] allProjectiles = FindObjectsOfType<Projectile>();
                foreach (Projectile projectile in allProjectiles)
                {
                    if (projectile.gameObject != gameObject)
                    {
                        Destroy(projectile.gameObject);
                    }
                }

                Camera.main.GetComponent<ShakeBehaviour>().TriggerShake();
            }
        }
        else if (gameObject.CompareTag("ProjectilePlayer2"))
        {
            if (collision.CompareTag("Player1"))
            {
                PlayerScoreManager.UpdatePlayerScore("Player1");
                Destroy(gameObject);
                SoundManager.Instance.playDeathSound();

                if (isPlayerOne)
                {
                    heartScript.subtractHealth();
                }

                // Destroy all other projectiles in the scene
                Projectile[] allProjectiles = FindObjectsOfType<Projectile>();
                foreach (Projectile projectile in allProjectiles)
                {
                    if (projectile.gameObject != gameObject)
                    {
                        Destroy(projectile.gameObject);
                    }
                }

                Camera.main.GetComponent<ShakeBehaviour>().TriggerShake();
            }
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