using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{

    
    public Movement p1;
    public MovementP2 p2;
    public ButtonSpawner spawner;
     int counter = 2;

    public GameObject[] buttons;
    public GameObject[] cannons;


    public float timeLimit = 5;
    public float time = 0;

    private void Start()
    {
        spawner = GetComponent<ButtonSpawner>();
    }


    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= timeLimit && counter < 8)
        {

            spawner.SpawnButton(buttons, counter);
            counter++;
            time = 0;

        }

        if (p1.ReturnP1Health() == 0)
        {
            P2Score.Instance.AddScore();
            SceneManager.LoadScene("GameMenu");
            
            
        }

        if(p2.ReturnP2Health() == 0)
        {
            
            P1Score.Instance.AddScore();
            SceneManager.LoadScene("GameMenu");
        }
        
    }



}
