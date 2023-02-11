using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rig;
    [SerializeField] private GameObject projectilePrefab; // Add this line
    [SerializeField] private float speed = 1;
    [SerializeField] private float jumpPower = 1;
    [SerializeField] private bool isPlayerOne;

    private bool grounded;
    private bool ragdollActive;
    private Vector3 groundNormal = Vector3.right;
    private float startingTime;
    private Vector3 gravityVelocity;
    private float knockback;
    private float screenWidth;
    private Vector3 startPosition;

    float jump;
    private GameObject projectileReference; // Add this line

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        startPosition = transform.position;
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
    }

    private void FixedUpdate()
    {
        float movementInput;
        float jumpInput;
        if (isPlayerOne)
        {
            movementInput = Input.GetAxis("Horizontal");
            jumpInput = Input.GetAxis("Vertical");
        }
        else
        {
            movementInput = Input.GetAxis("Horizontal2");
            jumpInput = Input.GetAxis("Vertical2");
        }

        if (Input.GetKeyDown(KeyCode.Space)) // Add this line
        {
            ShootProjectile();
        }

        if (grounded && jumpInput > 0)
        {
            rig.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
        }
        transform.position = Vector3.MoveTowards(transform.position, transform.position + movementInput * groundNormal, Time.deltaTime * speed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (movementInput > 0)
            {
                // Shoot right
                GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                projectile.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10, ForceMode2D.Impulse);
            }
            else if (movementInput < 0)
            {
                // Shoot left
                GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                projectile.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 10, ForceMode2D.Impulse);
            }
        }
    }

    private void ShootProjectile() // Add this method
    {
        projectileReference = Instantiate(projectilePrefab, transform.position, transform.rotation);
    }

    public void ApplyKnockback(float knockback)
    {
        //this.knockback = knockback;
        rig.AddForce(Vector3.right * knockback, ForceMode2D.Impulse);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        ContactPoint2D[] contactPoints = new ContactPoint2D[1];
        collision.GetContacts(contactPoints);
        ContactPoint2D groundContactPoint = new ContactPoint2D();
        foreach (ContactPoint2D contactPoint in contactPoints)
        {
            if (contactPoint.collider.tag == "Rotating Paddle")
            {
                groundContactPoint = contactPoint;
                break;
            }
            else if (contactPoint.collider.tag != "player")
            {
                groundContactPoint = contactPoint;
            }
            else
            {
                groundContactPoint = contactPoint;
            }
        }

        groundNormal = Quaternion.Euler(0, 0, -90) * groundContactPoint.normal;
        //Debug.Log(groundContactPoint.collider.gameObject.name);
        if (groundContactPoint.normal.y < 0.60f)
        {
            grounded = false;
            groundNormal = Vector3.right;
            if (ragdollActive == false && collision.gameObject.tag == "Rotating Paddle")
            {
                //ragdollActive = true;
            }
        }
        else
        {
            startingTime = Time.timeSinceLevelLoad;
            ragdollActive = false;
            grounded = true;
            jump = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        groundNormal = Vector3.right;
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            transform.position = startPosition;

            GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
            foreach (GameObject projectile in projectiles)
            {
                Destroy(projectile);
            }
        }
        /*if (collision.gameObject.tag == "Projectile")
        {
            transform.position = startPosition;
            GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
            foreach (GameObject projectile in projectiles)
            {
                Destroy(projectile);
            }
        }*/
    }

}
