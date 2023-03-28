using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceCannon : MonoBehaviour
{
    [SerializeField] GameObject barrel;
    [SerializeField] ParticleSystem cannonFire;
    
    private GameTimer shootTimer;

    private void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
        shootTimer = new GameTimer(3 + Random.Range(-100, 100) / 100f, false);
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
            shootTimer.PauseTimer(false);
            if (cannonFire.isPaused)
            {
                cannonFire.Play();
            }
        }
        else if (newGameState == GameState.Paused)
        {
            enabled = false;
            shootTimer.PauseTimer(true);
            if (cannonFire.isPlaying)
            {
                cannonFire.Pause();
            }
        }
    }

    private void Update()
    {
        if (shootTimer.UpdateTimer())
        {
            Shoot();
            shootTimer.RestartTimer();
            shootTimer.SetTimeTillCompletion(3 + Random.Range(-100, 100) / 100f);
        }
    }

    public void Shoot()
    {
        GameObject newBarrel = Instantiate(barrel, transform.position, Quaternion.identity);
        newBarrel.GetComponent<Rigidbody2D>().AddForce(transform.right / (8 + Random.Range(0, 8)), ForceMode2D.Impulse);
        cannonFire.Play();
    }
}
