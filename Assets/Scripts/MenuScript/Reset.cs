using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : Singleton<Reset>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.J) && Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.L))
        {
            P1Score.Instance.ResetScore();
            P2Score.Instance.ResetScore();

            SoundManager.Instance.FadeGameMusic();
            
            StartCoroutine(SoundManager.Instance.fadeTitleMusicOut());
            SceneManager.LoadScene("StartMenu");
        }
    }
}
