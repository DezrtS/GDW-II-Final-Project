using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreUI2 : MonoBehaviour
{
    public TextMeshProUGUI scoreTextP2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreTextP2.text = P2Score.Instance.ReturnScore().ToString();
    }
}
