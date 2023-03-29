using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpawner : MonoBehaviour
{
    Vector2 RandomPos;
    //int whileBreaker = 0;

    public void SpawnButton(GameObject [] buttonsList, int counter)
    {
        
        
         RandomPos   = new Vector2(Random.Range(-12.5f, 12.5f), Random.Range(-5.5f, 5.5f));

       /* for(int i = 0; i < counter; i++)
        {
            while(buttonsList[i].transform.position.x - RandomPos.x  < 1 || buttonsList[i].transform.position.y - RandomPos.x < 1  || whileBreaker < 5)
            {
                whileBreaker++;
                Debug.Log("While");
                RandomPos = new Vector2(Random.Range(-12.5f, 12.5f), Random.Range(-5.5f, 5.5f));
            }
        }*/
       
        buttonsList[counter].transform.position = RandomPos;


    }



    //Vector2 randomSpawn = new Vector2(Random.Range(-5, 5), Random.Range(-4, 4));



    /*
    bool enter = true;
    public GameObject button;
    

    public void SpawnButton()
    {
        enter = true;

        if (enter )
        {
            
           
            
            
          
           Instantiate(button, randomSpawn, Quaternion.identity);
            enter = false;
            
        }
    }
    */
}


