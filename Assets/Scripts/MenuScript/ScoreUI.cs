using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    
    public TextMeshProUGUI scoreTextP1;
    //public Text scoreTextP2;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        


        scoreTextP1.text = P1Score.Instance.ReturnScore().ToString();
       // scoreTextP2.text = P2Score.Instance.ReturnScore().ToString();
    }
}
