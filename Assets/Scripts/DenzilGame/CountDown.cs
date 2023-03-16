using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    AnimationClip clip;

    private void Awake()
    {

        /*
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Time.timeScale = Mathf.Min(1, Time.timeScale + 0.1f);
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Time.timeScale = Mathf.Max(0.1f, Time.timeScale - 0.1f);
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        */
    }

    private void OnDisable()
    {
        StopGame();
    }

    private void OnEnable()
    {
        StartGame();
    }

    public void StartGame()
    {
        Time.timeScale = 1;
    }

    public void StopGame()
    {
        Time.timeScale = 0;
    }
}
