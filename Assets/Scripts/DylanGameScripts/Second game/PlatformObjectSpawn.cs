using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformObjectSpawn : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public float spawnInterval = 2f;

    private GameObject[] platforms;

    void Start()
    {
        platforms = GameObject.FindGameObjectsWithTag("Platform");
        InvokeRepeating("SpawnPrefabOnRandomPlatform", 0f, spawnInterval);
    }

    void SpawnPrefabOnRandomPlatform()
    {
        int randomPlatformIndex = Random.Range(0, platforms.Length);
        GameObject platform = platforms[randomPlatformIndex];

        Vector2 spawnPosition = platform.transform.position + new Vector3(Random.Range(-platform.transform.localScale.x / 2f, platform.transform.localScale.x / 2f),
                                                                        Random.Range(-platform.transform.localScale.y / 2f, platform.transform.localScale.y / 2f),
                                                                        0f);

        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }

}
