using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    private float screenWidth;
    private Vector3 originalVelocity;

    private void Start()
    {
        screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        originalVelocity = GetComponent<Rigidbody2D>().velocity;
    }

    private void OnBecameInvisible()
    {
        if (transform.position.x > screenWidth)
        {
            transform.position = new Vector3(-screenWidth, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -screenWidth)
        {
            transform.position = new Vector3(screenWidth, transform.position.y, transform.position.z);
        }
        else if (transform.position.y > Camera.main.orthographicSize)
        {
            transform.position = new Vector3(transform.position.x, -Camera.main.orthographicSize, transform.position.z);
        }
        else if (transform.position.y < -Camera.main.orthographicSize)
        {
            transform.position = new Vector3(transform.position.x, Camera.main.orthographicSize, transform.position.z);
        }
    }


    private void Update()
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.Space))
        {
            direction += transform.right;
        }
        if (Input.GetKey(KeyCode.R))
        {
            direction += Vector3.up;
        }
        transform.position += direction * speed * Time.deltaTime;
    }

}