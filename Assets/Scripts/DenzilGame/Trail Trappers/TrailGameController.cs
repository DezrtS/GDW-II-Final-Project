using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrailGameController : MonoBehaviour
{
    public static TrailGameController instance;

    [SerializeField] private TrailMovement playerOneMovement;
    [SerializeField] private TrailMovement playerTwoMovement;

    [SerializeField] public GameObject playerOneTrailPrefab;
    [SerializeField] public GameObject playerTwoTrailPrefab;

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
            GameEnder.instance.StartEndGame();
            //StartCoroutine(LoadMainMenuReset());
        }
    }

    IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(1f);
        playerOneMovement.heartsKeeper.resetHealths = false;
        playerOneMovement.heartsKeeper.canTakeAwayHealth = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator LoadMainMenuReset()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("GameMenu");
    }
}
