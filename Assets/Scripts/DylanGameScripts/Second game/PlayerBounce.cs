using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounce : MonoBehaviour
{
    [SerializeField] private float minBouncePower;
    [SerializeField] private float maxBouncePower;
    [SerializeField] private float powerIncrease;

    private float bouncePower;

    public Rigidbody2D body;


    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        bouncePower = minBouncePower;
        body.sharedMaterial.bounciness = bouncePower;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        IncreaseBouncePower();
        /*if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            int numCollisions = collision.contacts.Length;
            bouncePower = Mathf.Clamp(bouncePower + numCollisions * powerIncrease, minBouncePower, maxBouncePower);

            Vector2 direction = (body.position - collision.rigidbody.position).normalized;
            body.velocity = direction * bouncePower;
            collision.rigidbody.velocity = -direction * bouncePower;
        }*/
    }

    void IncreaseBouncePower()
    {
        bouncePower = bouncePower + powerIncrease;
        body.sharedMaterial.bounciness = bouncePower;
    }
}
