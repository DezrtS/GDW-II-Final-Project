using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadScene : MonoBehaviour
{
    [SerializeField] private float zoomTransitionDuration = 2f;
    [SerializeField] private Vector3 zoomInto = Vector3.zero;
    [SerializeField] private bool doTransitionOnStart = false;
    [SerializeField] private bool resetMusicOnTransition = false;

    private int loadScene;
    private bool loading;

    private void Start()
    {
        if (doTransitionOnStart)
        {
            if (!SoundManager.Instance.Title.isPlaying)
            {
                StartCoroutine(SoundManager.Instance.fadeTitleMusicSoundIn());
            }
            CameraManager.Instance.ZoomCameraTo(9, zoomTransitionDuration);
            CameraManager.Instance.MoveCameraTo(new Vector3(0, 0, 0), zoomTransitionDuration);
        }
    }

    public void LoadNextScene()
    {
        switch (loadScene)
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
            case 7:
                SceneManager.LoadScene("Credits");
                break;
            case 8:
                SceneManager.LoadScene("Controls");
                break;
            case 9:
                SceneManager.LoadScene("GameMenu");
                break;
            case 10:
                SceneManager.LoadScene("StartMenu");
                break;
            case 11:
                SceneManager.LoadScene("DangerDodgeBallTutorial");
                break;
            case 12:
                SceneManager.LoadScene("PivotPanicTutorial");
                break;
            case 13:
                SceneManager.LoadScene("BulletButtonTutorial");
                break;
            case 14:
                SceneManager.LoadScene("RicochetRubmleTutorial");
                break;
            case 15:
                SceneManager.LoadScene("SavageShooterTutorial");
                break;
            case 16:
                SceneManager.LoadScene("TrailTrapperTutorial");
                break;
            default:
                loading = false;
                break;
        }
    }

    public void SetNextSceneToLoad(int loadScene)
    {
        if (loading)
        {
            return;
        }
        
        if (CameraManager.Instance.ZoomCameraTo(0.1f, zoomTransitionDuration) && CameraManager.Instance.MoveCameraTo(zoomInto, zoomTransitionDuration))
        {
            loading = true;
            this.loadScene = loadScene;
            StartCoroutine(ZoomTransition());
            if (resetMusicOnTransition && loadScene < 7)
            {
                StartCoroutine(SoundManager.Instance.fadeTitleMusicOut());
            }
        }
    }

    public void LoadDangerDodgeball ()
    {
        SetNextSceneToLoad(0);
    }

    public void LoadPivotPanic()
    {
        SetNextSceneToLoad(1);
    }
    public void LoadButtonBullets()
    {
        SetNextSceneToLoad(2);
    }

    public void LoadPilotPush()
    {
        SetNextSceneToLoad(3);
    }

    public void LoadRichochetRumbel()
    {
        SetNextSceneToLoad(4);
    }

    public void LoadSavageShooter()
    {
        SetNextSceneToLoad(5);
    }

    public void LoadTrailTrapper()
    {
        SetNextSceneToLoad(6);
    }

    public void LoadDangerDodgeballTutorial()
    {
        SetNextSceneToLoad(11);
    }

    public void LoadPivotPanicTutorial()
    {
        SetNextSceneToLoad(12);
    }
    public void LoadButtonBulletsTutorial()
    {
        SetNextSceneToLoad(13);
    }


    public void LoadRichochetRumbleTutorial()
    {
        SetNextSceneToLoad(14);
    }

    public void LoadSavageShooterTutorial()
    {
        SetNextSceneToLoad(15);
    }

    public void LoadTrailTrapperTutorial()
    {
        SetNextSceneToLoad(16);
    }

    public void LoadCredits()
    {
        SetNextSceneToLoad(7);
    }

    public void LoadControls()
    {
        SetNextSceneToLoad(8);
    }

    public void LoadGameSelectMenu()
    {
        SetNextSceneToLoad(9);
    }

    public void ReplayGame()
    {
        SoundManager.Instance.fadeTitleMusicOut();
        P1Score.Instance.ResetScore();
        P2Score.Instance.ResetScore();
      //  Destroy(FindObjectOfType<SoundManager>());

        //Destroy(GameObject.Find("Sound"));
        SetNextSceneToLoad(9);
    }

    private IEnumerator ZoomTransition()
    {
        yield return new WaitForSeconds(zoomTransitionDuration);
        LoadNextScene();
    }

}
