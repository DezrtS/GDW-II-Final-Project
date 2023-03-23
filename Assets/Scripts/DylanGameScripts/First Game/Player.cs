using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
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
    private float screenWidth;
    private int score = 0;
    public Vector3 startingPosition;

    float jump;
    private GameObject projectileReference;

    private Animator anim;

    [SerializeField] Hearts heartScript;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        heartScript = gameObject.GetComponent<Hearts>();
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

            if (movementInput == 0 && grounded)
            {
                anim.SetBool("isRunning", false);
            }
            else
            {
                anim.SetBool("isRunning", true);
            }

            if (movementInput > 0)
            {
                transform.localScale = new Vector3(2, 2, 2); // Flip the sprite to face right
            }
            else if (movementInput < 0)
            {
                transform.localScale = new Vector3(-2, 2, 2); // Flip the sprite to face left
            }

        }
        else
        {
            movementInput = Input.GetAxis("Horizontal2");
            jumpInput = Input.GetAxis("Vertical2");

            if (movementInput == 0 && grounded)
            {
                anim.SetBool("isRunning2", false);
            }
            else
            {
                anim.SetBool("isRunning2", true);
            }

            if (movementInput > 0)
            {
                transform.localScale = new Vector3(2, 2, 2); // Flip the sprite to face right
            }
            else if (movementInput < 0)
            {
                transform.localScale = new Vector3(-2, 2, 2); // Flip the sprite to face left
            }

        }

        if (grounded && jumpInput > 0)
        {
            //anim.SetTrigger("Player1Jump");
            rig.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
            // anim.SetBool("isJumping", false);
        }
        else
        {
            //anim.SetBool("isJumping", true);
        }
        transform.position = Vector3.MoveTowards(transform.position, transform.position + movementInput * groundNormal, Time.deltaTime * speed);
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("Player1") || gameObject.CompareTag("Player2"))
        {
            if (collision.CompareTag("ProjectilePlayer1") || collision.CompareTag("ProjectilePlayer2"))
            {
                if ((isPlayerOne && collision.CompareTag("ProjectilePlayer2")) ||
                    (!isPlayerOne && collision.CompareTag("ProjectilePlayer1")))
                {
                    heartScript.subtractHealth();
                }
            }
        }

        if (heartScript.returnHealth() <= 0)
        {
            if (isPlayerOne)
            {
                Debug.Log("Player Two Wins");
                P2Score.Instance.AddScore();
                LoadMainMenuReset();
            }
            else
            {
                Debug.Log("Player One Wins");
                P1Score.Instance.AddScore();
                LoadMainMenuReset();
            }
            FreezeGame(true);
        }
    }

    public int ReturnP1Health()
    {
        return (heartScript.returnHealth());
    }

    public int ReturnP2Health()
    {
        return (heartScript.returnHealth());
    }

    public void FreezeGame(bool loadMainMenu)
    {
        if (loadMainMenu)
        {
            StartCoroutine(LoadMainMenuReset());
        }
    }

    IEnumerator LoadMainMenuReset()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("GameMenu");
    }
}
