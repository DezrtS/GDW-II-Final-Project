using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 10f;
     public Rigidbody2D  body;
    
    Vector2 move;

    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            move.x = -1;
        }

        else if(Input.GetKey(KeyCode.D))
        {
            move.x = 1;
        }

        else
        {
            move.x = 0;
        }
        // move.x = Input.GetAxisRaw("Horizontal");
        // move.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.W))
        {
            move.y = 1;
        }

        else if (Input.GetKey(KeyCode.S))
        {
            move.y = -1;
        }

        else
        {
            move.y = 0;
        }


    }

     void FixedUpdate()
    {
        body.MovePosition(body.position + move * playerSpeed * Time.fixedDeltaTime);
    }
}
