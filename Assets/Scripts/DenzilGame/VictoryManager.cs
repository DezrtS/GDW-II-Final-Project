using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryManager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(StartMusic());
    }

    IEnumerator StartMusic()
    {
        yield return new WaitForSeconds(2);
        StartCoroutine(SoundManager.Instance.fadeTitleMusicSoundIn());
        ConfettiManager.Instance.OnlyPlayConfetti();
    }
}
