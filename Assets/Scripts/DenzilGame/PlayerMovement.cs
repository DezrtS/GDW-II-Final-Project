using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rig;

    [SerializeField] private float maxSpeed = 5;
    [SerializeField] private float speed = 1;

    private float speedMultiplier = 1;

    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode leftKey;
    public KeyCode rightKey;

    bool ragdollActive;

    Vector3 groundNormal = Vector3.right;

    float startingTime;

    Vector3 lastGroundNormal = Vector3.right;
    float gravity = 0;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKey(leftKey))
        {
            //transform.position = transform.position - groundNormal * speed * speedMultiplier;
            rig.velocity = -groundNormal * speed;
        }
        if (Input.GetKey(rightKey))
        {
            //transform.position = transform.position + groundNormal * speed * speedMultiplier;
            rig.velocity = groundNormal * speed;
        }
        if (ragdollActive)
        {
            gravity += 0.001f;
        } else
        {
            gravity = 0.001f;
        }
        transform.position = transform.position + new Vector3(0, -gravity * Time.deltaTime, 0);

        /*
        if (Input.GetKey(rightKey))
        {
            float magnitude = ((Vector3)rig.velocity + speed * speedMultiplier * groundNormal).magnitude;
            if (magnitude > maxSpeed)
            {
                rig.velocity = maxSpeed * speedMultiplier * new Vector3(groundNormal.x, groundNormal.y - lastGroundNormal.y + rig.velocity.y, 0);
                //rig.velocity = maxSpeed * speedMultiplier * groundNormal;
            }
            else
            {
                rig.AddForce(speed * speedMultiplier * groundNormal, ForceMode2D.Force);
            }
        }
        if (Input.GetKey(leftKey))
        {
            float magnitude = ((Vector3)rig.velocity - speed * speedMultiplier * groundNormal).magnitude;
            if (magnitude > maxSpeed)
            {
                rig.velocity = -maxSpeed * speedMultiplier * new Vector3(groundNormal.x, -(groundNormal.y - lastGroundNormal.y + rig.velocity.y), 0);
                //rig.velocity = -maxSpeed * speedMultiplier * groundNormal;
            }
            else
            {
                rig.AddForce(-speed * speedMultiplier * groundNormal, ForceMode2D.Force);
            }
        }
        lastGroundNormal = groundNormal;
        */
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //if (collision.gameObject.tag == "Rotating Paddle")
        //{
            groundNormal = Quaternion.Euler(0, 0, -90) * collision.contacts[0].normal;
            if (Mathf.Abs(groundNormal.y) > 0.45f)
            {
                if (ragdollActive == false)
                {
                    //groundNormal = Vector3.zero;
                    //lastGroundNormal = Vector3.zero;
                    ragdollActive = true;
                }
                speedMultiplier = Mathf.Max(speedMultiplier - 0.01f, 0);
            }
            else
            {
                speedMultiplier = 1;
                ragdollActive = false;
            }
        //}
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //if (!ragdollActive && collision.gameObject.tag == "Rotating Paddle")
        //{
            groundNormal = Vector3.right;
        //} 
    }
}
