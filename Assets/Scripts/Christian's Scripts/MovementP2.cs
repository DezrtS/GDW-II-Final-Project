using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementP2 : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 10f;
    public Rigidbody2D body;

    [SerializeField] Hearts heartScript;

    Vector2 move;

   // int p2Health = 3;
    public Transform trans;
    public Vector2 faceDirection;

    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        heartScript = gameObject.GetComponent<Hearts>();
        trans = GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            move.x = -1;
            FaceForward();
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            move.x = 1;
            FaceForward();
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
            FaceForward();
        }

        else if (Input.GetKey(KeyCode.DownArrow))
        {
            move.y = -1;
            FaceForward();
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
            SoundManager.Instance.playHitSound();
            heartScript.subtractHealth();
            ShakeBehaviour.instance.TriggerShake();
        }
    }


    public int ReturnP2Health()
    {
        return (heartScript.returnHealth());
    }

    void FaceForward()
    {
        Vector2 movedirection = move;
        faceDirection = movedirection;
        // if(move.x == 0 && move.y == 0)
        //  { 


        Quaternion rotationdirection = Quaternion.LookRotation(Vector3.forward, faceDirection);
        trans.rotation = Quaternion.RotateTowards(trans.rotation, rotationdirection, 1);
    }
}
