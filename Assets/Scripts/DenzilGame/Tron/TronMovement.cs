using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class TronMovement : MonoBehaviour
{
    public bool isPlayerOne;
    string horizontal1 = "Horizontal", horizontal2 = "Horizontal2", vertical1 = "Vertical", vertical2 = "Vertical2", button1 = "Fire1", button2 = "Fire2";
    string horizontalInput, verticalInput, fireInput;

    Rigidbody2D rig;

    [SerializeField] float speed = 5;
    [SerializeField] float rotationSpeed = 5;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        SetupPlayerInput();
    }

    void Update()
    {
         
    }

    private void FixedUpdate()
    {
        Move();
    }

    void SetupPlayerInput()
    {
        if (isPlayerOne)
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

    void Move()
    {
        rig.MovePosition(rig.position + (Vector2)transform.up * speed * Time.fixedDeltaTime);
        transform.Rotate(0f, 0f, -Input.GetAxis(horizontalInput) * rotationSpeed, Space.Self);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Trail" || collision.gameObject.tag == "Player")
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
