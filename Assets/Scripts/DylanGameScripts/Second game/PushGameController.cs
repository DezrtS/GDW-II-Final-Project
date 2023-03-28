using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushGameController : Singleton<PushGameController>
{
    [SerializeField] GameObject[] players;

    public GameObject GetClosestPlayer(GameObject originalPlayer)
    {
        float distance = int.MaxValue;
        GameObject closestPlayer = null;
        foreach (GameObject player in players)
        {
            float newDistance = (player.transform.position - originalPlayer.transform.position).magnitude;
            if (player != originalPlayer && newDistance < distance)
            {
                distance = newDistance;
                closestPlayer = player;
            }
        }
        return closestPlayer;
    }
}
