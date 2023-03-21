using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Score : Singleton<P1Score>
{
    int score = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    public int ReturnScore()
    {
        return score;
    }

    public void AddScore()
    {
        score ++;
    }
}
