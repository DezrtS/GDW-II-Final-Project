using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    Rigidbody2D rig;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != gameObject && collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PivotPlayer>().ApplyKnockback((collision.transform.position - transform.position).normalized * 10);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Reset")
        {
            Destroy(gameObject);
        }
    }
}
