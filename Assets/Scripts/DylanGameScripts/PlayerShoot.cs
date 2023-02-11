using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Projectile projectilePrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) ;
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Projectile projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        projectile.GetComponent<Rigidbody2D>().velocity = transform.right * projectile.speed;
    }
}
