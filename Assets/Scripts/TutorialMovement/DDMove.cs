using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDMove : MonoBehaviour
{

    Rigidbody2D body;
    Transform trans;
    Animator animator;
    public float speed = 10;
    public float rotationStr;
    public float steeringInput;
    public float moveInput;
    public bool buttonInput;

    string horizontal1 = "Horizontal", horizontal2 = "Horizontal2", vertical1 = "Vertical", vertical2 = "Vertical2", button1 = "Fire1", button2 = "Fire2";
    string horizontalInput, verticalInput, fireInput;

    public Vector2 upDirection;

    public bool isPlayer1;


    // Start is called before the first frame update
    void Start()
    {
        StartGetInputs();
        body = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        animator.SetBool("isredplayer1", isPlayer1);

    }

    // Update is called once per frame
    void Update()
    {
        float x = trans.up.x;
        float y = trans.up.y;
        upDirection = new Vector2(x, y);
        moveInput = Input.GetAxis(verticalInput);
        steeringInput = Input.GetAxis(horizontalInput);
        moveInput = Input.GetAxis(verticalInput);
        buttonInput = Input.GetButtonDown(fireInput);
        AnimatorMethod();
    }

    private void FixedUpdate()
    {
  


        body.MovePosition(body.position + upDirection * moveInput * speed * Time.fixedDeltaTime);

        trans.Rotate(0f, 0f, -steeringInput * rotationStr, Space.Self);
    }

    void FindInputs()
    {
        steeringInput = Input.GetAxis(horizontalInput);
        moveInput = Input.GetAxis(verticalInput);
        buttonInput = Input.GetButtonDown(fireInput);
        
    }

    void StartGetInputs()
    {
        if (isPlayer1)
        {
            horizontalInput = horizontal1;
            verticalInput = vertical1;
            fireInput = button1;
        }
        else
        {
            horizontalInput = horizontal2;
            verticalInput = vertical2;
            fireInput = button2;
        }
    }
    void AnimatorMethod()
    {
        animator.SetFloat("animeinputvertical", moveInput);
    }
}
