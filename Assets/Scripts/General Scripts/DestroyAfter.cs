using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    [SerializeField] float destroyAfter;

    private GameTimer destroyTimer;

    private void Awake()
    {
        destroyTimer = new GameTimer(destroyAfter, false);
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        if (newGameState == GameState.Gameplay)
        {
            enabled = true;
            destroyTimer.PauseTimer(false);
        }
        else if (newGameState == GameState.Paused)
        {
            enabled = false;
            destroyTimer.PauseTimer(true);
        }
    }

    private void Update()
    {
        if (destroyTimer.UpdateTimer())
        {
            Destroy(gameObject);
        }
    }
}
