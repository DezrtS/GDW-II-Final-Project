using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateController : MonoBehaviour
{
    private Rigidbody2D rig;
    [SerializeField] private bool overrideRotation = false;
    [SerializeField] private bool startAtRandomRotation = false;
    [SerializeField] private float rotationsPerSecond;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        if (startAtRandomRotation)
        {
            transform.eulerAngles = new Vector3(0, 0, Random.Range(-30, 30));
        }
        rotationsPerSecond = (Random.Range(-10, 10) / 100f);
    }

    void Update()
    {
        if (overrideRotation)
        {
            rig.angularVelocity = rotationsPerSecond * 360;
        }
    }
}
