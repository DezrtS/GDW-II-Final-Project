using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour    
{
    public playermovementballgame movementscript;
    Rigidbody2D body;
    Transform trans;
    SpriteRenderer sprite;
    ParticleSystem partsys;
    public bool isRedPlayer1;
    public float mag;
    public float lastMag;
    public float targetMag;
    public float maxMag;
    public float inc;
    public Vector2 vecNorm;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
        sprite = GetComponent<SpriteRenderer>();
        partsys = GetComponent<ParticleSystem>();
        
        mag = body.velocity.magnitude;
        int randomNum = Random.Range(1, 10);
        if (randomNum < 6) 
        {
            isRedPlayer1 = false;
            sprite.color = Color.blue;
            trans.Translate(10,0,0);
        } 
        else if( randomNum > 5)
        {
            isRedPlayer1 = true;
            sprite.color = Color.red;
            trans.Translate(-10, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetMagnitude();
    }

    private void FixedUpdate()
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
                collision.gameObject.SetActive(false);
            }
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
}
