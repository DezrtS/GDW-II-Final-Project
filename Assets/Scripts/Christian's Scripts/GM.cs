using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{

    
    public Movement p1;
    public MovementP2 p2;
    public ButtonSpawner spawner;
     int counter = 2;

    public GameObject[] buttons;
    public GameObject[] cannons;

    public float timeLimit = 5;

    private GameTimer spawnerTimer;

    private void Awake()
    {

        spawnerTimer = new GameTimer(timeLimit, false);
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
            spawnerTimer.PauseTimer(false);
        }
        else if (newGameState == GameState.Paused)
        {
            enabled = false;
            spawnerTimer.PauseTimer(true);
        }
    }

    private void Start()
    {
        SoundManager.Instance.pauseTitleMusic();
        SoundManager.Instance.playGameMusicSound();
        spawner = GetComponent<ButtonSpawner>();
    }


    // Update is called once per frame
    void Update()
    {
        if (spawnerTimer.UpdateTimer() && counter < 8)
        {

            spawner.SpawnButton(buttons, counter);
            counter++;
            
            spawnerTimer.RestartTimer();

        }

        if (p1.ReturnP1Health() <= 0 && !GameEnder.Instance.IsGameEnding())
        {
            P2Score.Instance.AddScore();
            GameEnder.Instance.StartEndGame();
            
            
        }

        if(p2.ReturnP2Health() <= 0 && !GameEnder.Instance.IsGameEnding())
        {
            
            P1Score.Instance.AddScore();
            GameEnder.Instance.StartEndGame();
        }
        
    }



}
