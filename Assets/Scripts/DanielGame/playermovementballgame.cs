using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class playermovementballgame : MonoBehaviour
{
    [SerializeField] Ball ballScript;
    Collider2D hitBox;
    Rigidbody2D body;
    Transform trans;
    
    public SpriteRenderer sprite;
    //Attack values
    [SerializeField] GameObject attackBoxObject;
    Collider2D attackBox;
    public int attackTime;
    public int attackTimeMax;
    public int attackCoolOff;
    public int attackCoolOffMax;
    public Vector2 upDirection;
    
    //Movement values
    public float speed;
    public float rotationStr;
    public float steeringInput;
    public float moveInput;
    public bool buttonInput;
    public bool isPlayer1;
    
    // Start is called before the first frame update
    void Start()
    {
        hitBox = GetComponent<PolygonCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
        attackBox = attackBoxObject.GetComponentInChildren<PolygonCollider2D>();

        if (isPlayer1)
        {
            sprite.color = Color.red;
        }
        else
        {
            sprite.color = Color.blue;
        }
    }

    // Update is called once per frame
    void Update()
    {
        FindUp();
        FindInputs();
        Attack();
        
        // create an if statment/function to get only the axis that you need
    }

    void FixedUpdate()
    {
        Movement();
    }

    void Attack()
    {
        if (buttonInput && attackCoolOff == 0)
        {
            attackTime = attackTimeMax;
            attackCoolOff = attackCoolOffMax;
        }
        if (attackTime > 0)
        {
            attackBox.enabled = true;
            
            attackTime--;
        } 
        else
        {
            if(attackCoolOff > 0)
            {
                attackCoolOff--;
            }
            attackBox.enabled = false;
        }
        //if (attackBox.enabled)
        //{
        //    attackBoxObject.SetActive(true);
        //}
        //else
        //{
        //    attackBoxObject.SetActive(false);
        //}
    }
    void Movement()
    {
        body.MovePosition(body.position + upDirection * moveInput * speed * Time.fixedDeltaTime);
        //body.MoveRotation(body.rotation + (-steeringInput1 * rotationStr));
        trans.Rotate(0f,0f,-steeringInput * rotationStr,Space.Self);


        //added hitbox off/on
        //if (ballScript.isRedPlayer1 == isPlayer1)
        //{
        //    hitBox.enabled = false;
        //}
        //else
        //{
        //    hitBox.enabled = true;
        //}
    }
    public Vector2 FindUp()
    {
        float x = trans.up.x;
        float y =trans.up.y;
        upDirection = new Vector2(x, y);
        return upDirection;
    }
    void FindInputs()
    {
        if (isPlayer1)
        {
            steeringInput = Input.GetAxis("Horizontal");
            moveInput = Input.GetAxis("Vertical");
            buttonInput = Input.GetButtonDown("Fire1");
        }
        else
        {
            steeringInput = Input.GetAxis("Horizontal2");
            moveInput = Input.GetAxis("Vertical2");
            buttonInput = Input.GetButtonDown("Fire2");
        }
    }

}