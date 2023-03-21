using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Score : Singleton<P2Score>
{

    int score = 0;

    public int ReturnScore()
    {
        return score;
    }

    public void AddScore()
    {
        score ++;
    }



}
