using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    public Movement p1;
    public MovementP2 p2;
    //public Button spawner;


   // public float timeLimit = 5;
   // public float time = 0;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
       // time += Time.deltaTime;

       // if (time >= timeLimit)
       // {

          //  spawner.SpawnButton();
          //  time = 0;

       // }

        if (p1.ReturnP1Health() == 0)
        {
            Debug.Log("p1 loses, game done");
        }

        if(p2.ReturnP2Health() == 0)
        {
            Debug.Log("p2 loses, game done");
        }
        
    }



}
