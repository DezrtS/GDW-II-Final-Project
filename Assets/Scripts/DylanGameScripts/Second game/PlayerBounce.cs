using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounce : MonoBehaviour
{
    private Rigidbody2D body;
    private PhysicsMaterial2D originalMaterial;
    private float originalBounciness;
    //private bool isBouncing = false;
    private Camera mainCamera;

    public float bouncePower;
    public GameObject otherPlayer; 

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        originalMaterial = body.sharedMaterial;
        originalBounciness = originalMaterial.bounciness;
        mainCamera = Camera.main;
    }

    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (Input.GetKey(KeyCode.Return))
        {
            if (collision.gameObject == otherPlayer) 
            {
                Debug.Log("Collision detected with other player");
                collision.gameObject.GetComponent<PushMovement>().KnockBack((collision.transform.position - transform.position).normalized, 0.5f);
                    //GetComponent<Rigidbody2D>().AddForce((collision.transform.position - transform.position).normalized * bounceIncrease * 10f, ForceMode2D.Impulse);
                //bounceIncrease = bounceIncrease * 2;

                //var material = new PhysicsMaterial2D();
                //material.bounciness = collision.gameObject.GetComponent<Rigidbody2D>().sharedMaterial.bounciness * bounceIncrease;
                //collision.gameObject.GetComponent<Rigidbody2D>().sharedMaterial = material;
            }
        }
    }
}
/*public class PlayerBounce : MonoBehaviour
{

    [SerializeField] private float minBouncePower;
    [SerializeField] private float maxBouncePower;
    [SerializeField] private float powerIncrease;

    private float bouncePower;

    private bool isBouncing = false;

    public Rigidbody2D body;

    public float bounceIncrease = 2;

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
        BouncePlayer();
        // Calculate the direction of the collision
        /*Vector2 direction = (transform.position - collision.transform.position).normalized;

            // Add a force in the direction of the collision to both players' rigidbodies
            float forceMagnitude = 10f;
            body.AddForce(direction * forceMagnitude, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(-direction * forceMagnitude, ForceMode2D.Impulse);
    }

    void BouncePlayer()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            Debug.Log("Collision detected");
            var material = new PhysicsMaterial2D();
            material.bounciness = body.sharedMaterial.bounciness * bounceIncrease;
            body.sharedMaterial = material;
        }
    }

    /*void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player1")
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
    }

    void StopBounce()
    {
        isBouncing = false;
    }
}*/
