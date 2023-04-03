using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPMove : MonoBehaviour
{
    private string horizontal1 = "Horizontal", horizontal2 = "Horizontal2", vertical1 = "Vertical", vertical2 = "Vertical2", button1 = "Fire1", button2 = "Fire2";
    private string horizontalInputString, verticalInputString, actionInputString;

    [SerializeField] private float speed = 1;
    [SerializeField] private float jumpPower = 1;

    private Rigidbody2D rig;

    private bool grounded;
    private Vector3 groundNormal = Vector3.right;
    private Vector3 normal = Vector3.right;
    private Vector3 knockback;

    private bool isWalking = false;
    private bool keepRotation = false;

    private float horizontalInput;
    private float verticalInput;
    [SerializeField] private bool isPlayerOne;
    [SerializeField] private Animator playerAnimator;
    // Start is called before the first frame update


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

    void SetupPlayerInput()
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
        

        if (Mathf.Abs(knockback.x) > 1f)
        {
            horizontalInput = 0;
        }

        if (horizontalInput != 0 && grounded)
        {
            if (!isWalking)
            {
                isWalking = true;
                playerAnimator.SetBool("IsWalking", isWalking);
            }
        }
        else
        {
            if (isWalking)
            {
                isWalking = false;
                playerAnimator.SetBool("IsWalking", isWalking);
            }
        }
        if (!keepRotation)
        {
            if (horizontalInput > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (horizontalInput < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }

        Vector3 horizontalVelocity = (horizontalInput * speed * Time.fixedDeltaTime * groundNormal) + (knockback);

        if (grounded && verticalInput > 0)
        {
            rig.velocity = new Vector2(rig.velocity.x, 0);
            rig.AddForce(jumpPower * Vector2.up, ForceMode2D.Impulse);
            grounded = false;
        }

        if (grounded)
        {
            rig.velocity = horizontalVelocity + new Vector3(0, -0.5f, 0);
        }
        else
        {
            if (Mathf.Abs(rig.velocity.x) > 1)
            {
                rig.velocity = new Vector2(horizontalVelocity.x, rig.velocity.y);
            }
            else
            {
                rig.AddForce(new Vector2(horizontalInput * normal.x, 0), ForceMode2D.Impulse);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Vector2 groundContactNormal = Vector2.right;
        for (int i = 0; i < collision.contacts.Length; i++)
        {
            if (collision.contacts[i].normal.y > 0.5f)
            {
                groundContactNormal = collision.contacts[i].normal;
                break;
            }
        }

        groundNormal = Quaternion.Euler(0, 0, -90) * groundContactNormal;
        if (groundContactNormal.y < 0.5f)
        {
            groundNormal = Vector3.right;
        }
        else
        {
            grounded = true;
        }
        normal = groundNormal;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
        groundNormal = Vector3.right;
        grounded = false;
    }

}