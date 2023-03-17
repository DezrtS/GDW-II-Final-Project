using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTopDownShootBullet : MonoBehaviour
{
    playermovementballgame playermovementballgame;
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
                bulletNum++;
                Respawn();
            }
        }
    }
    void Shoot()
    {
        if(playermovementballgame.buttonInput && bulletNum > 0)
        {
            GameObject bullet = Instantiate(Bullet, shootPosition.position,shootPosition.rotation);
            bulletNum--;

            Physics2D.IgnoreLayerCollision(8, 3);
        }
    }
    void Respawn()
    {
        transform.position = startPosition;
        hearts.subtractHealth();
        animator.Play("");
    }
}
