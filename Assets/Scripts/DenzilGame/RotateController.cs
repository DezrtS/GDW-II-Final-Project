using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateController : MonoBehaviour
{
    private Rigidbody2D rig;
    [SerializeField] float maxRotationSpeed;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rig.angularVelocity > maxRotationSpeed)
        {
            rig.angularVelocity = maxRotationSpeed;
        }
        else if (rig.angularVelocity < -maxRotationSpeed)
        {
            rig.angularVelocity = -maxRotationSpeed;
        }
    }

    private void LateUpdate()
    {
        if (rig.angularVelocity > maxRotationSpeed)
        {
            rig.angularVelocity = maxRotationSpeed;
        } else if (rig.angularVelocity < -maxRotationSpeed)
        {
            rig.angularVelocity = -maxRotationSpeed;
        }
    }
}
