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
            transform.eulerAngles = new Vector3(0, 0, Random.Range(-45, 45));
        }
        rotationsPerSecond = (Random.Range(2, 8) * Mathf.Sign(Random.Range(-1, 0)) / 100f);
    }

    void Update()
    {
        if (overrideRotation)
        {
            rig.angularVelocity = rotationsPerSecond * 360;
        }
    }
}
