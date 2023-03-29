using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnderUtility : MonoBehaviour
{
    private void OnEnable()
    {
        GameEnder.Instance.LoadMainMenuScene();
    }
}
