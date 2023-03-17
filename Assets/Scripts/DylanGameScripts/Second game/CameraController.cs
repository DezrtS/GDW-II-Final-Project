using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float radius = 5f;
    public float moveSpeed = 0.1f;
    public float moveTime = 3f;

    private Vector3 targetPosition;
    private bool isMoving = false;

    private void Start()
    {
        targetPosition = GetRandomPosition();
    }

    private void Update()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveToPosition(targetPosition));
        }
    }

    private IEnumerator MoveToPosition(Vector3 position)
    {
        isMoving = true;

        float elapsedTime = 0f;
        Vector3 startingPosition = transform.position;

        while (elapsedTime < moveTime)
        {
            transform.position = Vector3.Lerp(startingPosition, position, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = position;
        targetPosition = GetRandomPosition();
        isMoving = false;
    }

    private Vector3 GetRandomPosition()
    {
        Vector2 randomPoint = Random.insideUnitCircle * radius;
        Vector3 newPosition = transform.position + new Vector3(randomPoint.x, randomPoint.y, 0f);
        return newPosition;
    }
}


