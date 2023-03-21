using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrailGameController : MonoBehaviour
{
    public static TrailGameController instance;

    [SerializeField] private TronMovement playerOneMovement;
    [SerializeField] private TronMovement playerTwoMovement;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }  
    }

    public void ShortenOtherPlayerTail(bool isPlayerOne)
    {
        if (isPlayerOne)
        {
            playerTwoMovement.trail.SetTrailLength(2);
        } else
        {
            playerOneMovement.trail.SetTrailLength(2);
        }
    }

    public void FreezeGame()
    {
        playerOneMovement.canMove = false;
        playerTwoMovement.canMove = false;
        ShakeBehaviour.instance.TriggerShake();
        StartCoroutine(ResetGame());
    }

    public void FreezeGame(bool loadMainMenu)
    {
        playerOneMovement.canMove = false;
        playerTwoMovement.canMove = false;
        ShakeBehaviour.instance.TriggerShake();
        if (loadMainMenu)
        {
            StartCoroutine(LoadMainMenuReset());
        }
    }

    IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(1f);
        playerOneMovement.heartsKeeper.resetHealths = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator LoadMainMenuReset()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("GameMenu");
    }
}
