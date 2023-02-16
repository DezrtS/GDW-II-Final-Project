using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpawner : MonoBehaviour
{


    public void SpawnButton(GameObject [] buttonsList, int counter)
    {

        buttonsList[counter].transform.position = new Vector2(Random.Range(-5, 5), Random.Range(-4, 4));



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


