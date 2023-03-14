using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [SerializeField] private List<GameObject> players = new List<GameObject>();
    [SerializeField] private List<GameObject> cannons = new List<GameObject>();
    [SerializeField] private List<GameObject> platformEnds = new List<GameObject>();
    [SerializeField] private GameObject rotatingPlatform;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

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
