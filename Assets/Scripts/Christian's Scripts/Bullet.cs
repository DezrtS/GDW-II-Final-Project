using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    Rigidbody2D body;
    Transform trans;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
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
            body.simulated = true;
        }
        else if (newGameState == GameState.Paused)
        {
            enabled = false;
            body.simulated = false;
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
     }

    

    void FaceForward()
    {
        Vector2 movedirection = body.velocity;
        Quaternion rotationdirection = Quaternion.LookRotation(Vector3.forward, movedirection);
        trans.rotation = Quaternion.RotateTowards(trans.rotation, rotationdirection, 100);
    }


}
