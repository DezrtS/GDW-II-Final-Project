using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != gameObject && collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().ApplyKnockback((collision.transform.position - transform.position).normalized * 5);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Lava")
        {
            Destroy(gameObject, 2);
        }
    }
}
