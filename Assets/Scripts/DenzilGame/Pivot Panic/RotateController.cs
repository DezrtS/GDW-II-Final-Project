using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateController : MonoBehaviour
{
    private Rigidbody2D rig;

    [SerializeField] private bool randomizeRotation = true;

    private float rotationsPerSecond;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();

        if (randomizeRotation)
        {
            transform.eulerAngles = new Vector3(0, 0, Random.Range(-45, 45));
        }
        rotationsPerSecond = (Random.Range(2, 7) * Mathf.Sign(Random.Range(-1, 1)) / 100f);

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

    private void FixedUpdate()
    {
        rig.angularVelocity = rotationsPerSecond * 360;
    }
}
