using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreManager : MonoBehaviour
{
    public static event Action<string> OnScoreUpdate;

    public static void UpdatePlayerScore(string playerName)
    {
        if (OnScoreUpdate != null)
        {
            OnScoreUpdate(playerName);
        }
    }
}

