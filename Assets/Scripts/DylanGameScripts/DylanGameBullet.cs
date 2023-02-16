using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class DylanGameBullet : MonoBehaviour
{
    Transform trans;
    Rigidbody2D body;
    public bool isVertical;
    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BulletLoop();
            
    }

    void BulletLoop()
    {
            //inacurrate x loop on diagonals WORKING JUST INACURRATE
            //if (speedx != 0 && speedy != 0)
            //{
            //    trans.position = new Vector3(-trans.position.x, -trans.position.y, trans.position.z);
            //}
            //else
            if (isVertical)
            {
                trans.position = new Vector3(-trans.position.x, trans.position.y, trans.position.z);
            }
            else //if (!isVertical) //just in case you want to use it this way
            {
                trans.position = new Vector3(trans.position.x, -trans.position.y, trans.position.z);
            }
    }
}
