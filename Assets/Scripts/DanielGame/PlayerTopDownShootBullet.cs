using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTopDownShootBullet : MonoBehaviour
{
    playermovementballgame playermovementballgame;
    bulletsUI bulletsUI;
    //ricohetBulletScript ricohetBulletScript;
    [SerializeField] GameObject Bullet;
    public Transform shootPosition;

    [SerializeField] Animator animator;
    Hearts hearts;

    Vector2 startPosition;
    public int bulletNum;

    // Start is called before the first frame update
    void Start()
    {
        bulletsUI = GetComponent<bulletsUI>();
        playermovementballgame = GetComponent<playermovementballgame>();
        startPosition = playermovementballgame.transform.position;
        hearts = GetComponent<Hearts>();
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.tag == "Bullet")
        {
            ricohetBulletScript ricohetBulletScript = collision.gameObject.GetComponent<ricohetBulletScript>();
            if(ricohetBulletScript.canKill)
            {
                Destroy(collision.gameObject);
                AmmoChange(true);
                Respawn();
            }
        }
    }
    void Shoot()
    {
        if(playermovementballgame.buttonInput && bulletNum > 0)
        {
            GameObject bullet = Instantiate(Bullet, shootPosition.position,shootPosition.rotation);
            AmmoChange(false);

            Physics2D.IgnoreLayerCollision(8, 3);
        }
    }
    public void AmmoChange(bool isInc)
    {
        if (isInc)
        {
            bulletNum++; bulletsUI.AddAmmo();
        }
        else
        {
            bulletNum--; bulletsUI.subtractAmmo();
        }
    }
    void Respawn()
    {
        transform.position = startPosition;
        hearts.subtractHealth();
        animator.Play("");
    }
}
