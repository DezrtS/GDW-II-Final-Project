using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float radius = 5f;
    public float moveSpeed = 0.1f;
    public float cooldownTime = 3f;

    private Vector3 targetPosition;
    private bool isMoving = false;
    private float cooldownTimer = 0f;

    private void Start()
    {
        targetPosition = GetRandomPosition();
    }

    private void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
                cooldownTimer = cooldownTime;
            }
        }
        else
        {
            if (cooldownTimer > 0)
            {
                cooldownTimer -= Time.deltaTime;
            }
            else
            {
                targetPosition = GetRandomPosition();
                isMoving = true;
            }
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector2 randomPoint = Random.insideUnitCircle * radius;
        Vector3 newPosition = transform.position + new Vector3(randomPoint.x, randomPoint.y, 0f);
        return newPosition;
    }
}

