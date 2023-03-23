using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Projectile projectilePrefab;
    private float timeSinceLastShot;
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
            anim.SetTrigger("isShooting2");
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
            Projectile projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            projectile.GetComponent<Rigidbody2D>().velocity = (transform.right * transform.localScale.x) * projectile.speed;
        }
    }


    /*private void Shoot()
    {
        if (Time.time - timeSinceLastShot >= 3f)
        {
            timeSinceLastShot = Time.time;
            Projectile projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            projectile.GetComponent<Rigidbody2D>().velocity = transform.localScale.x * Vector2.right * projectile.speed;
        }
    }*/


    private void ShootVertically()
    {
        if (Time.time - timeSinceLastShot >= 3f)
        {
            Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = Vector2.up * projectile.speed;
        }
    }

}
