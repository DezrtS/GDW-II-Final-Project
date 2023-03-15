using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float radius = 5f;
    public float moveSpeed = 1f;
    public float cooldownTime = 3f;

    private Vector3 targetPosition;
    private bool isMoving = false;
    private float cooldownTimer = 0f;

    private void Start()
    {
        // Start with a random target position
        targetPosition = GetRandomPosition();
    }

    private void Update()
    {
        if (isMoving)
        {
            // Move towards the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Check if we've reached the target position
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
                cooldownTimer = cooldownTime;
            }
        }
        else
        {
            // Check if we need to start moving again
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
        // Get a random point inside a circle with radius 'radius'
        Vector2 randomPoint = Random.insideUnitCircle * radius;
        Vector3 newPosition = transform.position + new Vector3(randomPoint.x, randomPoint.y, 0f);
        return newPosition;
    }
}

