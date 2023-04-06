using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotGameController : MonoBehaviour
{
    public static PivotGameController Instance;


    [SerializeField] private List<GameObject> players = new List<GameObject>();
    [SerializeField] private HeartsKeeper heartsKeeper;

    public bool onTutorial = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        TransitionManager.Instance.OnTransitionEnded -= OnTransitionEnded;
    }

    private void OnTransitionEnded(bool isExitTransition)
    {
        if (isExitTransition && !onTutorial)
        {
            GameUIManager.Instance.ShowUI();
            CountdownManager.Instance.SpawnAndStartCountdown();
        }
    }

    private void Start()
    {
        TransitionManager.Instance.OnTransitionEnded += OnTransitionEnded;

        if (!onTutorial)
        {
            if (heartsKeeper.isNewGame)
            {
                StartCoroutine(SoundManager.Instance.fadeSideViewMusicIn());
                TransitionManager.Instance.PlayRandomExitTransition();
            }
            else
            {
                GameUIManager.Instance.ShowUINow();
                CountdownManager.Instance.RestartCountdown();
            }
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
