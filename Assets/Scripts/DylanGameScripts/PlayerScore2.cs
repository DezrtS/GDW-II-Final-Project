using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore2 : MonoBehaviour
{
    public Text player2ScoreText;
    private int player2Score = 0;

    private void OnEnable()
    {
        PlayerScoreManager.OnScoreUpdate += UpdateScoreText;
    }

    private void OnDisable()
    {
        PlayerScoreManager.OnScoreUpdate -= UpdateScoreText;
    }

    private void UpdateScoreText(string playerName)
    {
        if (playerName == "Player2")
        {
            player2Score++;
            player2ScoreText.text = "Player 2 Hits: " + player2Score;
        }
    }
}
