using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class playermovementballgame : MonoBehaviour
{
    Rigidbody2D body;
    Transform trans;
    public float speed;
    public float rotationStr;
    public float steeringInput;
    
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        steeringInput = Input.GetAxis("Horizontal"); 
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.F))
        {
            UnityEngine.Debug.Log(trans.up);
        }
        if (Input.GetKey(KeyCode.W))
        {
            trans.position= trans.position + trans.up*speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            trans.position = trans.position + trans.up*-speed;
        }
        body.MoveRotation(body.rotation + (-steeringInput * rotationStr));
    }
}
