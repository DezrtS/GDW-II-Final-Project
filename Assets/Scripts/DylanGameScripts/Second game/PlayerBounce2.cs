using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounce2 : MonoBehaviour
{
    private bool isBouncing = false;

    public Rigidbody2D body;


    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>(); ;
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected");
        // Calculate the direction of the collision
        Vector2 direction = (transform.position - collision.transform.position).normalized;

            // Add a force in the direction of the collision to both players' rigidbodies
            float forceMagnitude = 10f;
            body.AddForce(direction * forceMagnitude, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(-direction * forceMagnitude, ForceMode2D.Impulse);
    }

    /*void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player2")
        {
            // Calculate the direction of the collision
            Vector2 direction = collision.contacts[0].normal;

            // Add a force in the direction of the collision to the player's rigidbody
            float forceMagnitude = 10f;
            body.AddForce(direction * forceMagnitude, ForceMode2D.Impulse);

            float bounce = 600f;
            body.AddForce(collision.contacts[0].normal * bounce);
            isBouncing = true;
            Invoke("StopBounce", 0.3f);
        }
    }*/

    void StopBounce()
    {
        isBouncing = false;
    }
}
