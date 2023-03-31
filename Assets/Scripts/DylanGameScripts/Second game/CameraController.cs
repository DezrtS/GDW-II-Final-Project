using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float radius = 5f;
    public float moveSpeed = 0.1f;
    public float moveTime = 3f;

    private Vector3 targetPosition;

    private GameTimer moveTimer;

    private void Awake()
    {
        moveTimer = new GameTimer(3, false);

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
            moveTimer.PauseTimer(false);
        }
        else if (newGameState == GameState.Paused)
        {
            enabled = false;
            moveTimer.PauseTimer(true);
        }
    }

    private void Start()
    {
        targetPosition = GetRandomPosition();
    }

    private void Update()
    {
        if (!moveTimer.UpdateTimer())
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / moveSpeed);
        } else
        {
            moveTimer.RestartTimer();
            targetPosition = GetRandomPosition();
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector2 randomPoint = Random.insideUnitCircle * radius;
        Vector3 newPosition = transform.position + new Vector3(randomPoint.x, randomPoint.y, 0f);
        return newPosition;
    }
}


