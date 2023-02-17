using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    SpriteRenderer sprite;
    public Cannon canNum;
    
    

    void Start()
    {

        sprite = GetComponent<SpriteRenderer>();
        
      

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.tag == "P1")
        {
          canNum.SetTarget(2);
            sprite.color = Color.red;
            
        }

       if (collision.gameObject.tag == "P2")
        {
        
          canNum.SetTarget(1);
           sprite.color = Color.blue;
        }
     

       if(collision.gameObject.tag == "Button")
        {
            
                gameObject.transform.position = gameObject.transform.position +  new Vector3(1,0);
        }

       

    }



}
