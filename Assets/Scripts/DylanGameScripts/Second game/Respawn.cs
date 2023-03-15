using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
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
    }
}


