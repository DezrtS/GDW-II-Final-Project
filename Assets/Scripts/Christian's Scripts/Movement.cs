using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 10f;
     public Rigidbody2D  body;

    [SerializeField] Hearts heartScript;


    int p1Health;
    
    Vector2 move;

    public void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        heartScript = gameObject.GetComponent<Hearts>();
    }

    // Update is called once per frame
    public void Update()
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

     public void FixedUpdate()
    {
        body.MovePosition(body.position + move * playerSpeed * Time.fixedDeltaTime);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            heartScript.subtractHealth();

            //Hearts.Instance.subtractHealth();


        }
    }


    public int ReturnP1Health()
    {
        return( heartScript.returnHealth());
    }



}
