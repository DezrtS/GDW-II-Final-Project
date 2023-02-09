using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementP2 : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 10f;
    public Rigidbody2D body;

    Vector2 move;

    int p2Health = 3;

    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            move.x = -1;
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            move.x = 1;
        }

        else
        {
            move.x = 0;
        }
        // move.x = Input.GetAxisRaw("Horizontal");
        // move.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.UpArrow))
        {
            move.y = 1;
        }

        else if (Input.GetKey(KeyCode.DownArrow))
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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            p2Health--;
        }
    }


    public int ReturnP2Health()
    {
        return p2Health;
    }
}
