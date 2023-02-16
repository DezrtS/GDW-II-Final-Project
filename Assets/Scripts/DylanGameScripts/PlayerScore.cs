using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    public Text playerText;
    int score = 0;

    // Update is called once per frame
    void Update()
    {
        playerText.text = "Player 1 Score: ";
    }
}
