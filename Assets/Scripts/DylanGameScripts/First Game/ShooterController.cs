using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(SoundManager.Instance.fadeSideViewMusicIn());
    }
}