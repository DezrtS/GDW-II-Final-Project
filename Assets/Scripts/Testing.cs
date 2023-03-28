using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    int count;
    GameTimer timer;
    [SerializeField] Animator otherAnimator;

    private void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
        timer = new GameTimer(2f, false);
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
            timer.PauseTimer(false);
            GetComponent<Animator>().speed = 1;
            otherAnimator.speed = 1;
        }
        else if (newGameState == GameState.Paused)
        {
            enabled = false;
            timer.PauseTimer(true);
            GetComponent<Animator>().speed = 0;
            otherAnimator.speed = 0;
        }
    }

    private void Update()
    {
        if (timer.UpdateTimer())
        {
            PrintTimeFinished();
            timer.RestartTimer();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GetComponent<Animator>().SetBool("TestBool", true);
            otherAnimator.SetBool("IsAttacking", true);
        } else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GetComponent<Animator>().SetBool("TestBool", false);
            otherAnimator.SetBool("IsAttacking", false);
        }
    }

    public void PrintTimeFinished()
    {
        //Debug.Log("Time Finished, Count = " + count);
        count++;
    }

}
