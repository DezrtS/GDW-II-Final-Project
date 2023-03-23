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
        if (freezeTime)
        {
            Time.timeScale = 0;
            ShakeBehaviour.instance.RemoveShake();
            freezeTimeAnimator.SetBool("CanEndGame", true);
        } else
        {
            StartCoroutine(EndGameIn());
        }

        // Play End Game Animation Here... (Adjust waitFor variable and/or EndGame animation length) (Make sure animation is Time.scaled independent)
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
