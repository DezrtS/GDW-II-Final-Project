using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryOnPlay : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 0.1f);
    }
}
