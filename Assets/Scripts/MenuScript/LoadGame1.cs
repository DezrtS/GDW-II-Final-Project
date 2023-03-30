using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadGame1 : MonoBehaviour
{
    private int loadGame;
    private bool loading;

    private void Awake()
    {
        TransitionManager.Instance.OnTransitionEnded += OnTransitionEnded;
    }

    private void OnDestroy()
    {
        TransitionManager.Instance.OnTransitionEnded -= OnTransitionEnded;
    }

    private void OnTransitionEnded(bool isExitTransition)
    {
        if (!isExitTransition)
        {
            LoadNextGame();
        }
    }

    private void Start()
    {
        if (!SoundManager.Instance.Title.isPlaying)
        {
            StartCoroutine(SoundManager.Instance.fadeTitleMusicSoundIn());
        }
    }

    public void LoadNextGame()
    {
        switch (loadGame)
        {
            case 0:
                SceneManager.LoadScene("DangerDodgeBall");
                break;
            case 1:
                SceneManager.LoadScene("PivotPanic");
                break;
            case 2:
                SceneManager.LoadScene("Bullet Buttons");
                break;
            case 3:
                SceneManager.LoadScene("PilotPush");
                break;
            case 4:
                SceneManager.LoadScene("RicohetRumble");
                break;
            case 5:
                SceneManager.LoadScene("SavageShooter");
                break;
            case 6:
                SceneManager.LoadScene("TrailTrappers");
                break;
            default:
                loading = false;
                break;
        }
    }

    public void SetNextGameToLoad(int loadGame)
    {
        if (loading)
        {
            return;
        }
        loading = true;
        this.loadGame = loadGame;
        TransitionManager.Instance.PlayRandomEnterTransition();
    }

    public void LoadDangerDodgeball ()
    {
        SetNextGameToLoad(0);
    }

    public void LoadPivotPanic()
    {
        SetNextGameToLoad(1);
    }
    public void LoadButtonBullets()
    {
        SetNextGameToLoad(2);
    }

    public void LoadPilotPush()
    {
        SetNextGameToLoad(3);
    }

    public void LoadRichochetRumbel()
    {
        SetNextGameToLoad(4);
    }

    public void LoadSavageShooter()
    {
        SetNextGameToLoad(5);
    }

    public void TrailTrapper()
    {
        SetNextGameToLoad(6);
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

}
