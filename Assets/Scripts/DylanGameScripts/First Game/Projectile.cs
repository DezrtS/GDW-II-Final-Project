using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    private float screenWidth;

    [SerializeField] private bool isPlayerOne;

    [SerializeField] Hearts heartScript;

    private void Start()
    {
        screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        heartScript = gameObject.GetComponent<Hearts>();
        //Destroy(gameObject, lifeTime);
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