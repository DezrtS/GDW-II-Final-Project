using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreIncreaseManager : Singleton<ScoreIncreaseManager>
{
    [SerializeField] TextMeshProUGUI redScore;
    [SerializeField] TextMeshProUGUI blueScore;

    private void Awake()
    {
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        redScore.text = P1Score.Instance.ReturnScore().ToString();
        blueScore.text = P2Score.Instance.ReturnScore().ToString();
    }
}
