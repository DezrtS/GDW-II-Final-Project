using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    Rigidbody2D body;
    Transform trans;

    private GameTimer destroyTimer;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
        destroyTimer = new GameTimer(3f, true);
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    private void Start()
    {
        destroyTimer.PauseTimer(false);
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        if (newGameState == GameState.Gameplay)
        {
            enabled = true;
            body.simulated = true;
            destroyTimer.PauseTimer(false);
        }
        else if (newGameState == GameState.Paused)
        {
            enabled = false;
            body.simulated = false;
            destroyTimer.PauseTimer(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "P1")
        {
           
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "P2")
        {
            
            Destroy(gameObject);
        }
    }

     void Update()
     {
        FaceForward();

        if (destroyTimer.UpdateTimer())
        {
            destroyTimer.RestartTimer();
            destroyTimer.PauseTimer(true);
            Destroy(gameObject);
        }
     }

    

    void FaceForward()
    {
        Vector2 movedirection = body.velocity;
        Quaternion rotationdirection = Quaternion.LookRotation(Vector3.forward, movedirection);
        trans.rotation = Quaternion.RotateTowards(trans.rotation, rotationdirection, 100);
    }


}
