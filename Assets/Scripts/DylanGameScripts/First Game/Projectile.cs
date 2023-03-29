using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rig;

    public float speed = 10f;
    private float screenWidth;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();

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
        }
        else if (newGameState == GameState.Paused)
        {
            enabled = false;
            rig.simulated = false;
        }
    }

    private void Start()
    {
        screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

    private void FixedUpdate()
    {
        if (transform.position.x > screenWidth)
        {
            transform.position = new Vector3(-screenWidth, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -screenWidth)
        {
            transform.position = new Vector3(screenWidth, transform.position.y, transform.position.z);
        }
        //else if (transform.position.y > Camera.main.orthographicSize)
        //{
        //    transform.position = new Vector3(transform.position.x, -Camera.main.orthographicSize, transform.position.z);
        //}
        //else if (transform.position.y < -Camera.main.orthographicSize)
        //{
        //    transform.position = new Vector3(transform.position.x, Camera.main.orthographicSize, transform.position.z);
        //}
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
}