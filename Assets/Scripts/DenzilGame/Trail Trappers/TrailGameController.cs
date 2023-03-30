using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrailGameController : MonoBehaviour
{
    public static TrailGameController Instance;

    [SerializeField] private TrailMovement playerOneMovement;
    [SerializeField] private TrailMovement playerTwoMovement;

    [SerializeField] public GameObject playerOneTrailPrefab;
    [SerializeField] public GameObject playerTwoTrailPrefab;

    [SerializeField] private GameObject playerOneTrailExtender;
    [SerializeField] private GameObject playerTwoTrailExtender;

    [SerializeField] private HeartsKeeper heartsKeeper;

    private GameTimer resetGameTimer;

    private bool isResetting;

    private void Awake()
    {
        resetGameTimer = new GameTimer(1.5f, true);

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
        if (isExitTransition)
        {
            GameUIManager.Instance.ShowUI();
            CountdownManager.Instance.SpawnAndStartCountdown();
        }
    }

    private void Start()
    {

        TransitionManager.Instance.OnTransitionEnded += OnTransitionEnded;

        if (heartsKeeper.isNewGame)
        {

            StartCoroutine(SoundManager.Instance.fadeTrailRumbleMusicIn());
            TransitionManager.Instance.PlayRandomExitTransition();
        }
        else
        {
            GameUIManager.Instance.ShowUINow();
            CountdownManager.Instance.RestartCountdown();
        }

       
    }

    private void Update()
    {
        if (resetGameTimer.UpdateTimer())
        {
            playerOneMovement.heartsKeeper.resetHealths = false;
            playerOneMovement.heartsKeeper.canTakeAwayHealth = true;
            resetGameTimer.RestartTimer();
            resetGameTimer.PauseTimer(true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }
    }

    public void ResetGame()
    {
        if (!isResetting)
        {
            resetGameTimer.PauseTimer(false);
            isResetting = true;
            FreezePlayers();
        }
    }

    public void FreezePlayers()
    {
        playerOneMovement.FreezePlayer();
        playerTwoMovement.FreezePlayer();
    }

    public bool IsResetting()
    {
        return isResetting;
    }
}
