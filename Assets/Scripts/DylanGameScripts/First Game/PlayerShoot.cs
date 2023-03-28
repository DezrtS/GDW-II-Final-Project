using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Projectile projectilePrefab;
    private float timeSinceLastShot = -3;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Return)) 
        {
            Shoot();
        }

        /*if (Input.GetKeyDown(KeyCode.Space) && Input.GetKeyDown(KeyCode.UpArrow))
        {
            ShootVertically();
        }*/
    }

    private void Shoot()
    {
        if (Time.time - timeSinceLastShot >= 3f)
        {
            timeSinceLastShot = Time.time;
            float facingDirection = Mathf.Sign(transform.localScale.x);
            Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, 90 * -facingDirection));
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(facingDirection * projectile.speed, 0f);
            anim.SetTrigger("isShooting2");
            SoundManager.Instance.playShootSound();
            //Debug.Log("Scale x: " + transform.localScale.x);

        }
    }

    private void ShootVertically()
    {
        if (Time.time - timeSinceLastShot >= 3f)
        {
            Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = Vector2.up * projectile.speed;
        }
    }

}
