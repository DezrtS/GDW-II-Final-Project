using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Camera mainCamera;

    int player1Health = 3;
    int player2Health = 3;

    [SerializeField] private bool isPlayerOne;

    [SerializeField] Hearts heartScript;


    private void Start()
    {
        mainCamera = Camera.main;
        heartScript = gameObject.GetComponent<Hearts>();
    }

    private void OnBecameInvisible()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            return;
        }

        Vector3 screenPos = mainCamera.WorldToScreenPoint(transform.position);

        if (screenPos.x < 0 || screenPos.x > Screen.width || screenPos.y < 0 || screenPos.y > Screen.height)
        {
            RespawnPlayer();
        }
    }

    private void RespawnPlayer()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            return;
        }

        Vector3 cameraPos = mainCamera.transform.position;
        transform.position = new Vector3(cameraPos.x, cameraPos.y, transform.position.z);

        if (isPlayerOne)
        {
            heartScript.subtractHealth();
        }
        else
        {
            heartScript.subtractHealth();
        }
    }

    public int ReturnP1Health()
    {
        return (heartScript.returnHealth());
    }

    public int ReturnP2Health()
    {
        return (heartScript.returnHealth());
    }
}


