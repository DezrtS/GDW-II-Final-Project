using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        

        //After player wins a game
        P1Score.Instance.AddScore(1);
  

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
