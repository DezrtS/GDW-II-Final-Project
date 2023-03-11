using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    public Text player1ScoreText;

    private int player1Score = 0;

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
        if (playerName == "Player1")
        {
            player1Score++;
            player1ScoreText.text = "Player 1 Hits: " + player1Score;
        }
    }
}
