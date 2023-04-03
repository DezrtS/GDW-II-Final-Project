using UnityEngine;

public class GameUIManager : Singleton<GameUIManager>
{
    private Animator gameUIAnimator;

    [SerializeField] private bool includeBullets;

    private void Awake()
    {
        gameUIAnimator = GetComponent<Animator>();
    }

    public void ShowUINow()
    {
        if (includeBullets)
        {
            gameUIAnimator.SetBool("ShowUINow", true);
        } 
        else
        {
            gameUIAnimator.SetBool("ShowUINow", true);
        }
        
    }

    public void ShowUI()
    {
        if (includeBullets)
        {
            gameUIAnimator.SetBool("ShowUIPlusBullets", true);
        } 
        else
        {
            gameUIAnimator.SetBool("ShowUI", true);
        }
    }

    public void HideUI()
    {
        gameUIAnimator.SetBool("HideUI", true);
    }

    public void IncreaseScore(bool redScore)
    {
        if (redScore)
        {
            gameUIAnimator.SetBool("IncreaseRedScore", true);
        } 
        else
        {
            gameUIAnimator.SetBool("IncreaseBlueScore", true);
        }
    }

    public void ShrinkScores()
    {
        gameUIAnimator.SetBool("ShrinkScores", true);
    }
}
