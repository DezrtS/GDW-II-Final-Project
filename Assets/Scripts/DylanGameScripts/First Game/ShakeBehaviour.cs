using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBehaviour : MonoBehaviour
{
    public static ShakeBehaviour Instance;

    // Desired duration of the shake effect
    public float shakeDuration = 0f;
    // Daniel changed this to public(was private) so it wouldn't conflict with the countdown animation
    // A measure of magnitude for the shake. Tweak based on your preference
    private float shakeMagnitude = 0.2f;

    // A measure of how quickly the shake effect should evaporate
    private float dampingSpeed = 1.0f;

    // The initial position of the GameObject
    Vector3 initialPosition;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void OnEnable()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }

    public void TriggerShake()
    {
        shakeDuration = 0.3f;
    }

    public void RemoveShake()
    {
        shakeDuration = 0f;
        transform.localPosition = initialPosition;
    }
}
