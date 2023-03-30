using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RumbleController : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(SoundManager.Instance.fadeTrailRumbleMusicIn());
    }
}
