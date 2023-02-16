using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateController : MonoBehaviour
{
    private Rigidbody2D rig;
    [SerializeField] private bool overrideRotation = false;
    [SerializeField] private float rotationsPerSecond = 0;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (overrideRotation)
        {
            rig.angularVelocity = rotationsPerSecond * 360;
        }
    }
}
