using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HeartsKeeper : ScriptableObject
{
    public bool resetHealths = true;
    public bool otherPlayerReset = false;
    public bool canTakeAwayHealth = true;
    public bool otherPlayerTakenDamage = false;
    public static int resetHealthValue = 3;
    public int playerOneHealth = 3;
    public int playerTwoHealth = 3;
    public bool isNewGame = true;

    public void ResetHealth()
    {
        playerOneHealth = resetHealthValue;
        playerTwoHealth = resetHealthValue;
        otherPlayerTakenDamage = false;
        canTakeAwayHealth = true;
        isNewGame = true;
    }

    public int GetHealth(bool isPlayerOne)
    {
        if (isPlayerOne)
        {
            return playerOneHealth;
        } else
        {
            return playerTwoHealth;
        }
    }

    public void TakeAwayHealth(bool isPlayerOne)
    {
        if (isPlayerOne)
        {
            playerOneHealth--;
        }
        else
        {
            playerTwoHealth--;
        }
        isNewGame = false;
    }

    public bool BothPlayersAlive()
    {
        return (playerOneHealth > 0 && playerTwoHealth > 0);
    }
}
