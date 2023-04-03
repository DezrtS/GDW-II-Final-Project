using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTmove : MonoBehaviour
{
    private string horizontal1 = "Horizontal", horizontal2 = "Horizontal2", vertical1 = "Vertical", vertical2 = "Vertical2", button1 = "Fire1", button2 = "Fire2";
    private string horizontalInputString, verticalInputString, actionInputString;

    private Rigidbody2D rig;

    [SerializeField] private float regularSpeed = 5;
    [SerializeField] private float fasterSpeed = 10;
    [SerializeField] private float rotationSpeed = 5;

    public bool canMove = true;
    public bool isPlayerOne;

    private float horizontalInput;
    private float verticalInput;


    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        
        SetupPlayerInput();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxis(verticalInputString);
        horizontalInput = Input.GetAxis(horizontalInputString);
    }

    private void SetupPlayerInput()
    {
        if (isPlayerOne)
        {
            horizontalInputString = horizontal1;
            verticalInputString = vertical1;
            actionInputString = button1;
        }
        else
        {
            horizontalInputString = horizontal2;
            verticalInputString = vertical2;
            actionInputString = button2;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float speed;
        if (verticalInput > 0)
        {
            speed = fasterSpeed;
        }
        else
        {
            speed = regularSpeed;
        }
        rig.MovePosition(rig.position + (Vector2)transform.up * speed * Time.fixedDeltaTime);
        transform.Rotate(0f, 0f, -horizontalInput * rotationSpeed, Space.Self);
    }
}
