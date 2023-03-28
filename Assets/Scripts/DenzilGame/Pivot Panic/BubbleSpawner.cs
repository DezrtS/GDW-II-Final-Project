using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField] private int spawningRange;
    [SerializeField] private GameObject bubble;

    [SerializeField] private float oscillationSpeed = 1;
    [SerializeField] private float oscillationStrength = 1;

    private GameTimer bubbleTimer;

    private void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
        bubbleTimer = new GameTimer(Random.Range(0.1f, 3f), false);
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
            bubbleTimer.PauseTimer(false);
        }
        else if (newGameState == GameState.Paused)
        {
            enabled = false;
            bubbleTimer.PauseTimer(true);
        }
    }

    private void Update()
    {
        if (bubbleTimer.UpdateTimer())
        {
            SpawnBubble();
            bubbleTimer.RestartTimer();
            bubbleTimer.SetTimeTillCompletion(Random.Range(0.1f, 3f));
        }
    }

    private void FixedUpdate()
    {
        transform.position = transform.position + new Vector3(0, oscillationStrength * Mathf.Sin(Time.timeSinceLevelLoad * oscillationSpeed), 0);
    }

    public void SpawnBubble()
    {
        Instantiate(bubble, new Vector3(Random.Range(-spawningRange, spawningRange), transform.position.y, 0), Quaternion.identity, transform);
    }
}
