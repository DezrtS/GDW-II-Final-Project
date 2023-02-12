using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public float timer = 90;

    private void Update()
    {
        timer -= Time.deltaTime;
        timerText.text = "" + (int)timer;

        if (timer < 86f && timer > 84.9f)
        {
            timerText.text = "SHOOT!";
        }

        if (timer <= 0f)
        {
            Application.Quit();
        }
    }
}
