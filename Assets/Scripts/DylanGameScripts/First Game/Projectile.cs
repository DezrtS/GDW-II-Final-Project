using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //Set Values
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
        //Get screen size
        screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

    private void FixedUpdate()
    {
        //Allow projectile to return back to the other side of the screen if they leave the camera
        if (transform.position.x > screenWidth)
        {
            transform.position = new Vector3(-screenWidth, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -screenWidth)
        {
            transform.position = new Vector3(screenWidth, transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check which projectile collided with which player
        if (gameObject.CompareTag("ProjectilePlayer1"))
        {
            if (collision.CompareTag("Player2"))
            {
                //Destroy the game object and play sound
                Destroy(gameObject);
                SoundManager.Instance.playDeathSound();

                Projectile[] allProjectiles = FindObjectsOfType<Projectile>();
                foreach (Projectile projectile in allProjectiles)
                {
                    if (projectile.gameObject != gameObject)
                    {
                        //Destroy all projectiles active on the screen
                        Destroy(projectile.gameObject);
                    }
                }

                //Shake the screen
                Camera.main.GetComponent<ShakeBehaviour>().TriggerShake();
            }
        }
        else if (gameObject.CompareTag("ProjectilePlayer2"))
        {
            if (collision.CompareTag("Player1"))
            {
                //Destroy the game object and play sound
                Destroy(gameObject);
                SoundManager.Instance.playDeathSound();

                Projectile[] allProjectiles = FindObjectsOfType<Projectile>();
                foreach (Projectile projectile in allProjectiles)
                {
                    if (projectile.gameObject != gameObject)
                    {
                        //Destroy all other projectiles in the scene
                        Destroy(projectile.gameObject);
                    }
                }

                //Shake the screen
                Camera.main.GetComponent<ShakeBehaviour>().TriggerShake();
            }
        }
    }
}