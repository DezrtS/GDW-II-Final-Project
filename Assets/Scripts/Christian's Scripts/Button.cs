using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    public Cannon can;
    SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.tag == "P1")
        {
            can.setTarget(2);
            sprite.color = Color.red;
            
        }

        if (collision.gameObject.tag == "P2")
        {
            can.setTarget(1);
            sprite.color = Color.blue;
        }

    }


    

    
}
