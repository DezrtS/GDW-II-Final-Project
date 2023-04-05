using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot1 : MonoBehaviour
{
    //Set shoot values
    public Projectile projectilePrefab;
    private float timeSinceLastShot = -3;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();

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
            anim.speed = 1;
        }
        else if (newGameState == GameState.Paused)
        {
            enabled = false;
            anim.speed = 0;
        }
    }

    private void Update()
    {
        //Shoot if shoot button was pressed
        if (Input.GetKey(KeyCode.F))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        //Allow players to only shoot 3 seconds after their last shot
        if (Time.time - timeSinceLastShot >= 3f)
        {
            //Shoot the projectile
            timeSinceLastShot = Time.time;
            float facingDirection = Mathf.Sign(transform.localScale.x);
            Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, 90 * -facingDirection));
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(facingDirection * projectile.speed, 0f);

            //Play animations/sound
            anim.SetTrigger("isShooting");
            SoundManager.Instance.playShootSound();
        }
    }
}
