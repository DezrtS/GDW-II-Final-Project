using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rig;
    [SerializeField] private float speed = 1;
    [SerializeField] private float jumpPower = 1;
    [SerializeField] private bool isPlayerOne;

    private bool grounded;
    private bool ragdollActive;
    private Vector3 groundNormal = Vector3.right;
    private float startingTime;
    private Vector3 gravityVelocity;
    private float knockback;

    float jump;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        
        float movementInput;
        float jumpInput;
        if (isPlayerOne)
        {
            movementInput = Input.GetAxis("Horizontal");
            jumpInput = Input.GetAxis("Vertical");
        } else
        {
            movementInput = Input.GetAxis("Horizontal2");
            jumpInput = Input.GetAxis("Vertical2");
        }
        /*
        if (!ragdollActive)
        {
            knockback = Mathf.Lerp(knockback, 0, Time.deltaTime * 3);
            if (Mathf.Abs(knockback) > 0.5f)
            {
                movementInput = 0;
            }
            Vector3 horizontalVelocity = (movementInput * speed * Time.fixedDeltaTime * groundNormal) + (knockback * Vector3.right);
            if (grounded && jumpInput > 0)
            {
                jump = 1;
            }
            Vector3 verticalVelocity = jump * jumpPower * Vector3.up;
            gravityVelocity = (Time.time - startingTime) * -10 * Vector3.up;
            rig.velocity = horizontalVelocity + verticalVelocity + gravityVelocity;
        } else
        {
            rig.constraints = RigidbodyConstraints2D.None;
        }
        */
        //Debug.Log(speed);

        if (grounded && jumpInput > 0)
        {
            rig.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
        }
        transform.position = Vector3.MoveTowards(transform.position, transform.position + movementInput * groundNormal, Time.deltaTime * speed);
    }

    public void ApplyKnockback(Vector3 knockback)
    {
        //this.knockback = knockback;
        rig.AddForce(knockback, ForceMode2D.Impulse);
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
            } else if (contactPoint.collider.tag != "player")
            {
                groundContactPoint = contactPoint;
            } else
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Lava")
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
