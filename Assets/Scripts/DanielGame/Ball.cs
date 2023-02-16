using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour    
{
    public playermovementballgame movementscript;
    public Collider2D ballHitBox;
    Rigidbody2D body;
    Transform trans;
    public SpriteRenderer sprite;
    ParticleSystem partsys;
    public bool isRedPlayer1;
    public float mag;
    public float lastMag;
    public float targetMag;
    public float maxMag;
    public float inc;
    public Vector2 vecNorm;
    public Vector3 start1 = new Vector3(-10, 0, 0);
    public Vector3 start2 = new Vector3(10, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        
        body = GetComponent<Rigidbody2D>();
        ballHitBox = GetComponent<Collider2D>();
        trans = GetComponent<Transform>();
        sprite = GetComponent<SpriteRenderer>();
        partsys = GetComponent<ParticleSystem>();
        
        mag = body.velocity.magnitude;
        
        int randomNum = Random.Range(1, 3);
        if (randomNum == 2) 
        {
            isRedPlayer1 = false;
            sprite.color = Color.blue;
            trans.position = start2;
        } 
        else if( randomNum == 1)
        {
            isRedPlayer1 = true;
            sprite.color = Color.red;
            trans.position = start1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetMagnitude();
    }

    void FixedUpdate()
    {
        BallSpeed();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        movementscript = collision.gameObject.GetComponentInParent<playermovementballgame>();
        //UnityEngine.Debug.Log("hit");
        sprite.color = movementscript.sprite.color;
        vecNorm = movementscript.FindUp();
        if(targetMag == 0)
        {
            targetMag = targetMag + inc*5;
        }
        targetMag = targetMag + inc;
        body.velocity = vecNorm * targetMag;
        partsys.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            movementscript = collision.gameObject.GetComponent<playermovementballgame>();
            
            if (movementscript.sprite.color != sprite.color)
            {
                Respawn(collision.gameObject);
                //collision.gameObject.SetActive(false);
            }
            //else if (movementscript.sprite.color == sprite.color)
            //{
            //    Physics2D.IgnoreCollision(movementscript.hitBox, ballHitBox);
            //}
        }
        GetMagnitude();
        if(mag != targetMag)
        {
            mag = targetMag;
        }
        //if (mag >= lastMag) 
        //{
        //    targetMag = mag; 
        //}
        //else
        //{
        //    targetMag = lastMag;
        //}
        //if (targetMag <= maxMag)
        //{
        //    //targetMag = targetMag + inc;
        //}
        //else
        //{
        //    targetMag = maxMag;
        //}
        
        BallSpeed();
        GetMagnitude();
        lastMag = mag;
    }

    void BallSpeed()
    {
        if (mag != targetMag)
        {
            mag = targetMag;
        }
        vecNorm = body.velocity.normalized;
        body.velocity = vecNorm*targetMag;
    }

    void GetMagnitude()
    {
        mag = body.velocity.magnitude;
        //UnityEngine.Debug.Log(body.velocity.magnitude); 
        //UnityEngine.Debug.Log(mag);
    }

    public void Respawn(GameObject player)
    {
        ////player reset
        //player.transform.position = movementscript.startPos;
        //player.transform.rotation = movementscript.startRot;
        //player.SetActive(true);
        ////ball reset
        //targetMag = 0;
        //body.velocity = new Vector2(0f, 0f);
        //movementscript = player.GetComponent<playermovementballgame>();
        //if (movementscript.sprite.color == Color.red)
        //{
        //    trans.position = start1;
        //    sprite.color = Color.red;
        //}
        //else if (movementscript.sprite.color == Color.blue)
        //{
        //    trans.position = start2;
        //    sprite.color = Color.blue;
        //}


        // | resets scene
        // V
        
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);Start();
    }
}
