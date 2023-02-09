using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpawner : MonoBehaviour
{

    public float timeLimit = 5;
    public float time = 0;

    bool enter = true;
    public GameObject button;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= timeLimit)
        {
            enter = true;
            SpawnButton();
            time = 0;

        }
    }



    public void SpawnButton()
    {
        if (enter)
        {
            time = 0;
            Vector2 randomSpawn = new Vector2(Random.Range(-5, 5), Random.Range(-4, 4));
            Instantiate(button, randomSpawn, Quaternion.identity);
            enter = false;
        }
    }
}


