using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingAnimation : MonoBehaviour
{
    public float frequency; // Speed of movement
    public float magnitude; // Range of movement
    public Vector3 direction;
    Vector3 initialPosition;

    private void Start()
    {
        // Save the starting point of the object
        initialPosition = transform.position;
    }

    void Update()
    {
        // Sine function for smooth floating effect
        transform.position = initialPosition + direction * Mathf.Sin(Time.time * frequency) * magnitude;
    }
}
