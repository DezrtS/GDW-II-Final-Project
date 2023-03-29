using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class P2Score : Singleton<P2Score>
{

    int score = 0;
    bool once = true;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(score == 3 && once)
        {
            SceneManager.LoadScene("VictoryBlue");
            once = false;
        }
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
