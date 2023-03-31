using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTopDownShootBullet : MonoBehaviour
{
    playermovementballgame playermovementballgame;
    bulletsUI bulletsUI;
    //ricohetBulletScript ricohetBulletScript;
    [SerializeField] GameObject Bullet;
    public Transform shootPosition;
    Hearts hearts;

    Vector2 startPosition;
    public int bulletNum;

    private void Awake()
    {
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
        }
        else if (newGameState == GameState.Paused)
        {
            enabled = false;
        }
    }

    void Start()
    {
        bulletsUI = GetComponent<bulletsUI>();
        playermovementballgame = GetComponent<playermovementballgame>();
        startPosition = playermovementballgame.transform.position;
        hearts = GetComponent<Hearts>();
    }

    void Update()
    {
        Shoot();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.tag == "Bullet")
        {
            ricohetBulletScript ricohetBulletScript = collision.gameObject.GetComponent<ricohetBulletScript>();
            if(ricohetBulletScript.canKill)
            {
                SoundManager.Instance.playDeathSound();
                Destroy(collision.gameObject);
                AmmoChange(true);
                Respawn();
            }
        }
    }
    void Shoot()
    {
        if(playermovementballgame.buttonInput && bulletNum > 0 && Time.timeScale == 1)
        {
            GameObject bullet = Instantiate(Bullet, shootPosition.position,shootPosition.rotation);
            AmmoChange(false);
            SoundManager.Instance.playShootSound();
            Physics2D.IgnoreLayerCollision(8, 3);
        }
    }
    public void AmmoChange(bool isInc)
    {
        if (isInc)
        {
            bulletNum++; bulletsUI.AddAmmo();
        }
        else
        {
            bulletNum--; bulletsUI.subtractAmmo();
        }
    }
    void Respawn()
    {
        transform.position = startPosition;
        hearts.subtractHealth();
        if (hearts.returnHealth() <= 0 && !GameEnder.Instance.IsGameEnding())
        {
            UnityEngine.Debug.Log("PLayer loses");
            ShakeBehaviour.Instance.TriggerShake();
            if (playermovementballgame.isPlayer1)
            {
                P2Score.Instance.AddScore();
            }
            else
            {
                P1Score.Instance.AddScore();
            }
            GameEnder.Instance.StartEndGame();
        }
        else if (!GameEnder.Instance.IsGameEnding())
        {
            ShakeBehaviour.Instance.TriggerShake();
            CountdownManager.Instance.RestartCountdown();
        } 
    }
}
