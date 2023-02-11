using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public float timer = 90f;

    private void Update()
    {
        timer -= Time.deltaTime;
        timerText.text = "" + (int)timer;

        if (timer <= 0f)
        {
            Application.Quit();
        }
    }
}
