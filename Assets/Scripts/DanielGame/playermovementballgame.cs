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
    Collider2D attackBox;
    public SpriteRenderer sprite;
    //[SerializeField] GameObject attackBox;
    public int attackTime;
    public int attackTimeMax;
    public float speed;
    public float rotationStr;
    public float steeringInput;
    public float moveInput;
    public bool buttonInput;
    public bool isPlayer1;
    public Vector2 upDirection;
    // Start is called before the first frame update
    void Start()
    {
        hitBox = GetComponent<PolygonCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
        attackBox = GetComponent<CircleCollider2D>();
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
        if (buttonInput)
        {
            attackTime = attackTimeMax;
        }
        if (attackTime > 0)
        {
            attackBox.enabled = true;
            
            attackTime--;
        } 
        else
        {
            attackBox.enabled = false;
        }
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