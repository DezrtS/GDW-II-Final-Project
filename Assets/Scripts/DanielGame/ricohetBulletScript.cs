using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;

public class ricohetBulletScript : MonoBehaviour
{
    playermovementballgame playermovementballgame;
    Rigidbody2D body;
    SpriteRenderer rend;
    Color color;
    Transform trans;
    Collider2D collider2D;
    float startTime;
    public float speed;
    public float LethalTime;
    public float LethalTimeMax;
    public bool canKill;
    public bool canPickup;

    float timeDifference = 0;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
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
            startTime = Time.time - LethalTime;
            timeDifference = 0;
        }
        else if (newGameState == GameState.Paused)
        {
            enabled = false;
            body.simulated = false;
            timeDifference = LethalTime;
        }
    }

    [SerializeField] Sprite active,inactive;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        trans = GetComponent<Transform>();
        collider2D = GetComponent<Collider2D>();
        body.velocity = (trans.up * speed);
        startTime = Time.time;
        StopBulletsColliding();
    }

    // Update is called once per frame
    void Update()
    {
        if (body.velocity != Vector2.zero)
        {
            FaceForward();
        }
        //else
        //{
        //    body.angularVelocity = 60;
        //}
        CheckLethalTime();
        //if (canKill)
        //{
        //    canPickup = false;
        //} 
        //else if (canPickup)
        //{
        //    canKill = false;
        //}
        //if(canKill || canPickup && Physics2D.GetIgnoreLayerCollision(8,3))
        //{
        //    Physics2D.IgnoreLayerCollision(8, 3, false);
        //}
        SpriteStuff();
    }

    private void FixedUpdate()
    {
        //AntiCollision();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.tag == "Bullet")
        //{
        //    Debug.Log("bullettest");
        //    ricohetBulletScript ricohetBullet = collision.gameObject.GetComponent<ricohetBulletScript>();
        //    Physics2D.IgnoreCollision(ricohetBullet.collider2D, collider2D, true);
        //}
        if (collision.gameObject.tag == "Untagged")
        {
            SoundManager.Instance.playBounceSound();
        }

        if (collision.gameObject.tag == "Untagged" && canKill == false)
        {
            SoundManager.Instance.playBounceSound();
            canKill = true;
            Physics2D.IgnoreLayerCollision(8, 3,false);
            
        }
        if(collision.gameObject.tag == "Player" && canPickup == true)
        {
            PlayerTopDownShootBullet playerTopDownShoot = collision.gameObject.GetComponent<PlayerTopDownShootBullet>();
            playerTopDownShoot.AmmoChange(true);
            SoundManager.Instance.playCountdownSound();
            Destroy(gameObject);
            
        }
        if(collision.gameObject.tag == "Player" && canKill == false && canPickup == false)
        {
            playermovementballgame = collision.gameObject.GetComponent<playermovementballgame>();
            Physics2D.IgnoreCollision(playermovementballgame.hitBox, collider2D, true);
        }
        //else if(canKill == true)
        //{
        //    playermovementballgame playermovementballgame = collision.gameObject.GetComponent<playermovementballgame>();
        //    Physics2D.IgnoreCollision(playermovementballgame.hitBox, collider2D, false);
        //}
    }

    void FaceForward()
    {
        Vector2 movedirection = body.velocity;
        quaternion rotationdirection = Quaternion.LookRotation(Vector3.forward, movedirection);
        trans.rotation = Quaternion.RotateTowards(trans.rotation, rotationdirection, 100);
    }

    void StopBulletsColliding()
    {
        Physics2D.IgnoreLayerCollision(3,3);
        //Physics2D.IgnoreLayerCollision(8,3);
    }
    void CheckLethalTime()
    {
        LethalTime = Time.time - startTime;
        if(LethalTime >= LethalTimeMax || speed == 0)
        {
            body.drag = 100;
            body.angularVelocity = 60;
            canKill = false;
            canPickup = true;
            if(playermovementballgame != null) {Physics2D.IgnoreCollision(playermovementballgame.hitBox, collider2D, false); }
            
        }
    }

    void SpriteStuff()
    {
        //color = Color.white;
        if (!canKill)
        {
            if (canPickup)
            {
                //color.a = 1f;
                rend.sprite = active;
            }
            else
            {
                //color.a = 0.5f;
                rend.sprite = inactive;
            }
            
        }
        else
        {
            //color.a = 1f;
            rend.sprite = active;
        }
        //rend.color = color;
    }
}
