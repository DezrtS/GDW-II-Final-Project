using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUp : MonoBehaviour
{
    public SpriteRenderer title;
    public SpriteRenderer titleLogo;
    float size = 0.01f;
    public AudioSource flicker;
    public AudioSource music;
    // public Button startButton;

    bool once = true;

    // Start is called before the first frame update
    void Start()
    {
        
        //title.enabled = false;
        titleLogo.enabled = false;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.orthographicSize > 9)
        {

            Camera.main.orthographicSize -= size;
        }

        if(Mathf.Round(Camera.main.orthographicSize) == 18)
        {
            titleLogo.enabled = true;
            flicker.Play();
            

        }

        if (Mathf.Round(Camera.main.orthographicSize) == 15)
        {
            titleLogo.enabled = false;


        }

      //  if (Mathf.Round(Camera.main.orthographicSize) == 15)
      //  {
        //    title.enabled = true;


      //  }

        if (Mathf.Round(Camera.main.orthographicSize) == 12)
        {
            titleLogo.enabled = true;
            flicker.Play();

        }

        if (Mathf.Round(Camera.main.orthographicSize) == 9 && once)
        {
            titleLogo.enabled = false;
            StartCoroutine(RunCoroutine());
           

            once = false;


        }

    }

    IEnumerator RunCoroutine()
    {

        yield return new WaitForSeconds(3);
        titleLogo.enabled = true;
        music.Play();
    }

    
}
