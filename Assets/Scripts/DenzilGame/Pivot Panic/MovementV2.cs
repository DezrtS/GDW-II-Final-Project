using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementV2 : MonoBehaviour
{
    private Rigidbody2D rig;
    [SerializeField] private float speed = 1;
    [SerializeField] private float jumpPower = 1;
    [SerializeField] private bool isPlayerOne;

    [SerializeField] Hearts heartScript;

    [SerializeField] HeartsKeeper heartsKeeper;
    [SerializeField] HeartsKeeperManager heartsKeeperManager;

    //[SerializeField] ParticleSystem groundedParticleSystem;
    

    private bool grounded;
    private Vector3 groundNormal = Vector3.right;
    Vector3 normal = Vector3.right;
    private Vector3 knockback;

    private void Start()
    {
        if (heartsKeeperManager.ResetHealths)
        {
            heartsKeeper.ResetHealth();
        }
        if (heartsKeeperManager.OtherPlayerReset)
        {
            heartsKeeperManager.ResetHealths = true;
            heartsKeeperManager.OtherPlayerReset = false;
        }
        else
        {
            heartsKeeperManager.OtherPlayerReset = true;
        }
        rig = GetComponent<Rigidbody2D>();
        heartScript = gameObject.GetComponent<Hearts>();
        heartScript.setHealth(heartsKeeper.health);
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

        knockback = knockback * Mathf.Lerp(1, 0, Time.deltaTime * 1.5f);

        if (Mathf.Abs(knockback.x) > 1f)
        {
            movementInput = 0;
        }

        Vector3 horizontalVelocity = (movementInput * speed * Time.fixedDeltaTime * groundNormal) + (knockback);

        if (grounded && jumpInput > 0)
        {
            rig.AddForce(jumpPower * Vector2.up, ForceMode2D.Impulse);
            grounded = false;
        }

        if (grounded)
        {
            rig.velocity = horizontalVelocity + new Vector3(0, -0.5f, 0);
        } else
        {
            if (Mathf.Abs(rig.velocity.x) > 1)
            {
                rig.velocity = new Vector2(horizontalVelocity.x, rig.velocity.y);
            } else
            {
                rig.AddForce(new Vector2(movementInput * normal.x, 0), ForceMode2D.Impulse);
            }
        }
    }

    public void ApplyKnockback(Vector3 knockback)
    {
        rig.AddForce(knockback);
        this.knockback = this.knockback + knockback;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.contacts[0].normal.x < 0.5f)
        //{
        //    groundedParticleSystem.Play();
        //}
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
            grounded = false;
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
        groundNormal = Vector3.right;
        grounded = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Lava")
        {
            rig.drag = 25;
            gameObject.layer = 12;
            heartScript.subtractHealth();
            heartsKeeper.health--;

        } else if (collision.gameObject.tag == "Reset")
        {
            if (heartScript.returnHealth() <= 0)
            {
                if (isPlayerOne)
                {
                    Debug.Log("Player Two Wins");
                } else
                {
                    Debug.Log("Player One Wins");
                }
            } else
            {
                heartsKeeperManager.ResetHealths = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
