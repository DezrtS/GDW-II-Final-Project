using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryManager : MonoBehaviour
{
    [SerializeField] private bool redWon;

    [SerializeField] private GameObject star;
    [SerializeField] private Color redColor;
    [SerializeField] private Color blueColor;

    private bool isReplaying = false;

    private void OnDestroy()
    {
        TransitionManager.Instance.OnTransitionEnded -= OnTransitionEnded;
    }

    private void OnTransitionEnded(bool isExitTransition)
    {
        if (isReplaying && !isExitTransition)
        {
            P1Score.Instance.ResetScore();
            P2Score.Instance.ResetScore();
            LoadMainMenuScene();
        }
    }

    void Start()
    {
        TransitionManager.Instance.OnTransitionEnded += OnTransitionEnded;
        if (redWon)
        {
            star.GetComponent<SpriteRenderer>().color = redColor;
        } else
        {
            star.GetComponent<SpriteRenderer>().color = blueColor;
        }
        StartCoroutine(StartMusic());
    }

    IEnumerator StartMusic()
    {
        yield return new WaitForSeconds(2);
        StartCoroutine(SoundManager.Instance.fadeTitleMusicSoundIn());
        ConfettiManager.Instance.OnlyPlayConfetti();
    }

    public void ReplayGame()
    {
        if (!isReplaying)
        {
            isReplaying = true;
            TransitionManager.Instance.PlayRandomEnterTransition();
        }
        //  Destroy(FindObjectOfType<SoundManager>());
        //Destroy(GameObject.Find("Sound"));
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("GameMenu");
    }
}
