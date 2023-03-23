using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnder : MonoBehaviour
{
    public static GameEnder instance;

    private Animator freezeTimeAnimator;

    [SerializeField] private bool freezeTime = false;

    [SerializeField] private float unfrozenWaitTime = 1f;

    private bool gameEnding = false;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        freezeTimeAnimator = GetComponent<Animator>();
    }

    public void StartEndGame()
    {
        gameEnding = true;
        if (freezeTime)
        {
            Time.timeScale = 0;
            if (ShakeBehaviour.instance != null)
            {
                ShakeBehaviour.instance.RemoveShake();
            }
            freezeTimeAnimator.SetBool("CanEndGame", true);
        } else
        {
            StartCoroutine(EndGameIn());
        }

        // Play End Game Animation Here... (Adjust waitFor variable and/or EndGame animation length) (Make sure animation is Time.scaled independent)
    }


    public bool IsGameEnding()
    {
        return gameEnding;
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("GameMenu");
    }

    IEnumerator EndGameIn()
    {
        yield return new WaitForSeconds(unfrozenWaitTime);
        LoadMainMenuScene();
    }
}
