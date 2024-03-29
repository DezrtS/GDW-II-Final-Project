using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounce : MonoBehaviour
{
    /*[SerializeField] private float minBouncePower;
    [SerializeField] private float maxBouncePower;
    [SerializeField] private float powerIncrease;

    private float bouncePower;*/

    private bool isBouncing = false;

    public Rigidbody2D body;


    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player1"))
        {
            float bounce = 600f;
            body.AddForce(collision.contacts[0].normal * bounce);
            isBouncing = true;
            Invoke("StopBounce", 0.3f);
        }
    }

    void StopBounce()
    {
        isBouncing = false;
    }
}
