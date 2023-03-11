using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed = 5f;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * cameraSpeed * Time.deltaTime;

        Vector3 pos = startPos;
        pos.x += Mathf.Sin(Time.time * cameraSpeed) * 2f; 
        transform.position = new Vector3(pos.x, transform.position.y, pos.z);  
    }
}
