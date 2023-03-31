using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{

    private Camera cam;
    private bool isZooming;
    private bool isMoving;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    public void MoveCameraTo(Vector2 targetPosition, float time)
    {
        if (isMoving)
        {
            return;
        }
        isMoving = true;
        StartCoroutine(MoveTo(targetPosition, time));
    }

    public void ZoomCameraTo(float size, float time)
    {
        if (isZooming)
        {
            return;
        }
        isZooming = true;
        StartCoroutine(ZoomTo(size, time));
    }

    private float SmoothValue(float startingTime, float timeTillCompletion)
    {
        return (0.5f * Mathf.Cos(((Time.timeSinceLevelLoad - startingTime - timeTillCompletion) * Mathf.PI) / timeTillCompletion) + 0.5f);
    }

    private IEnumerator MoveTo(Vector3 targetPosition, float time)
    {
        targetPosition = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);

        float startingTime = Time.timeSinceLevelLoad;
        Vector3 originalPosition = transform.position;
        Vector3 difference = targetPosition - transform.position;
        while (transform.position != targetPosition && Time.timeSinceLevelLoad - startingTime < time)
        {
            transform.position = originalPosition + difference * SmoothValue(startingTime, time);
            yield return null;
        }
        transform.position = targetPosition;
        isMoving = false;
    }

    private IEnumerator ZoomTo(float size, float time)
    {
        float startingTime = Time.timeSinceLevelLoad;
        float originalSize = cam.orthographicSize;
        float difference = size - cam.orthographicSize;
        while (cam.orthographicSize != size && Time.timeSinceLevelLoad - startingTime < time)
        {
            cam.orthographicSize = originalSize + difference * SmoothValue(startingTime, time);
            yield return null;
        }
        cam.orthographicSize = size;
        isZooming = false;
    }

}
