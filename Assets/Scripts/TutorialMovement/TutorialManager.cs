using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    private bool canExitTutorial = false;

    [SerializeField] private int sceneToLoad = 0;

    private void OnDestroy()
    {
        TransitionManager.Instance.OnTransitionEnded -= OnTransitionEnded;
    }

    private void OnTransitionEnded(bool isExitTransition)
    {
        if (isExitTransition)
        {
            canExitTutorial = true;
        } else
        {
            LoadScene();
        }
    }

    public void LoadScene()
    {
        switch (sceneToLoad)
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
                break;
        }

    }

    private void Start()
    {
        SoundManager.Instance.fadeTitleMusicSoundIn();
        TransitionManager.Instance.OnTransitionEnded += OnTransitionEnded;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canExitTutorial)
        {
            canExitTutorial = false;
            TransitionManager.Instance.PlayRandomEnterTransition();
        }
    }
}
