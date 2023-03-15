using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HeartsKeeper : ScriptableObject
{
    public static int resetHealthValue = 3;
    public int health = 3;

    public void ResetHealth()
    {
        health = resetHealthValue;
    }
}
