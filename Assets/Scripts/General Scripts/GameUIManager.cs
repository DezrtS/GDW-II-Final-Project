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
}
