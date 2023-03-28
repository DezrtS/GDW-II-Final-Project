using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadGame1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadDangerDodgeball ()
    {
        SceneManager.LoadScene("DangerDodgeBall");
    }

    public void LoadPivotPanic()
    {
        SceneManager.LoadScene("PivotPanic");
    }
    public void LoadButtonBullets()
    {
        SceneManager.LoadScene("Bullet Buttons");
    }

    public void LoadPilotPush()
    {
        SceneManager.LoadScene("PilotPush");
    }

    public void LoadRichochetRumbel()
    {
        SceneManager.LoadScene("RicohetRumble");
    }

    public void LoadSavageShooter()
    {
        SceneManager.LoadScene("SavageShooter");
    }

    public void TrailTrapper()
    {
        SceneManager.LoadScene("TrailTrappers");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

}
