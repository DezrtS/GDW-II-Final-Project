using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{

   // int target;
    
    public Transform spawn;
    public GameObject bulletPre;
    public float bulletSpeed;
    public Vector2 distance;
    [SerializeField] GameObject player1;



    [SerializeField] GameObject player2;
    public Vector2 distanceP2;



    [SerializeField] int target;

    [SerializeField] float timeLimit;
    public float time = 0;
    bool enter = false;

    void Start()
    {
        spawn = gameObject.GetComponent<Transform>();
        bulletPre = GameObject.Find("Bullet");
       
    }

    // Update is called once per frame
    void Update()
    {
        
        time += Time.deltaTime;
        enter = false;
        distance = new Vector2(player1.transform.position.x - spawn.position.x, player1.transform.position.y - spawn.position.y);



        distanceP2 = new Vector2(player2.transform.position.x - spawn.position.x, player2.transform.position.y - spawn.position.y);


        if (time >= timeLimit && !enter)
        {
            enter = true;
            time = 0;


            if (target == 1)
            {
                SoundManager.Instance.playShootSound();
                var bullet = Instantiate(bulletPre, spawn.position, Quaternion.Euler(0,0,0 ));
                bullet.GetComponent<Rigidbody2D>().velocity = distance * bulletSpeed;
                Destroy(bullet, 3);
            }


            if(target == 2)
            {
                SoundManager.Instance.playShootSound();
                var bulletP2 = Instantiate(bulletPre, spawn.position, Quaternion.Euler(0,0,0));
                bulletP2.GetComponent<Rigidbody2D>().velocity = distanceP2 * bulletSpeed;
               Destroy(bulletP2, 3);
            }



        }


    }

    public void SetTarget(int num)
    {
        target = num;
    }
}
