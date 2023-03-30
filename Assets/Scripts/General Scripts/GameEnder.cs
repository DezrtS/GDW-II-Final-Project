using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnder : MonoBehaviour
{
    public static GameEnder Instance;

    [SerializeField] private bool pauseGameOnEnd = false;

    private GameTimer hideUITimer;
    private GameTimer endGameTimer;

    private bool gameEnding = false;


    private void Awake()
    {
        hideUITimer = new GameTimer(1.1f, true);
        endGameTimer = new GameTimer(1, true);

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
        if (gameEnding && !isExitTransition)
        {
            LoadMainMenuScene();
        }
    }

    private void Start()
    {
        TransitionManager.Instance.OnTransitionEnded += OnTransitionEnded;
    }

    public void StartEndGame()
    {
        gameEnding = true;
        endGameTimer.PauseTimer(false);
        if (pauseGameOnEnd)
        {
            GameStateManager.Instance.SetState(GameState.Paused);
        }
    }

    private void Update()
    {
        if (endGameTimer.UpdateTimer())
        {
            if (!endGameTimer.GetTimerAlreadyFinished())
            {
                GameUIManager.Instance.HideUI();
                hideUITimer.PauseTimer(false);
            }


            if (hideUITimer.UpdateTimer())
            {
                TransitionManager.Instance.PlayRandomEnterTransition();
                endGameTimer.RestartTimer();
                endGameTimer.PauseTimer(true);
                hideUITimer.RestartTimer();
                hideUITimer.PauseTimer(true);
            }
        }
    }

    public bool IsGameEnding()
    {
        return gameEnding;
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("GameMenu");
    }
}
