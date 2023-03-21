using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField] private int spawningRange;
    [SerializeField] private GameObject bubble;

    [SerializeField] private float oscillationSpeed = 1;
    [SerializeField] private float oscillationStrength = 1;

    void Start()
    {
        StartCoroutine(SpawnBubble());
    }

    private void FixedUpdate()
    {
        transform.position = transform.position + new Vector3(0, oscillationStrength * Mathf.Sin(Time.timeSinceLevelLoad * oscillationSpeed), 0);
    }

    IEnumerator SpawnBubble()
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 3f));
        Instantiate(bubble, new Vector3(Random.Range(-spawningRange, spawningRange), transform.position.y, 0), Quaternion.identity, transform);
        StartCoroutine(SpawnBubble());
    }
}
