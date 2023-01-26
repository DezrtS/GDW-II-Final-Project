using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Transform spawn;
    public GameObject bulletPre;
    public float bulletSpeed;
    public Vector2 distance;
    [SerializeField] GameObject player1;
    [SerializeField] float timeLimit;
    public float time = 0;
    bool enter = false;

    void Start()
    {
        spawn = gameObject.GetComponent<Transform>();
       
    }

    // Update is called once per frame
    void Update()
    {
        
        time += Time.deltaTime;
        enter = false;
        distance = new Vector2(player1.transform.position.x - spawn.position.x, player1.transform.position.y - spawn.position.y);

        if (time >= timeLimit && !enter)
        {
            enter = true;
            time = 0;
            var bullet = Instantiate(bulletPre, spawn.position, spawn.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = distance * bulletSpeed;
            Destroy(bullet, 3);



        }

       
    }
}
