using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    public AudioSource buttonClick;
   

    private void Awake()
    {
       // DontDestroyOnLoad(gameObject);
       // ButtonDestroy();


    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayButtonClickSound()
    {
        FindObjectOfType<SoundManager>().playButtonClickSound();
    }

    //IEnumerator ButtonDestroy()
    ///{
    //    yield return new WaitForSeconds(3);
    //    Destroy(gameObject);
    //}
        
}
