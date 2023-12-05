using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Buoyancy : MonoBehaviour
{
    public float buoyancyFactor = 1.0f; // Adjust this to control buoyancy strength
    public float sinkFrequency = 2.0f;  // Adjust this to control the sinking frequency
    public float sinkAmplitude = 1.0f;  // Adjust this to control the sinking amplitude

    private Rigidbody2D rb;
    private float timeElapsed = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Adjust the gravity scale based on the buoyancy factor
        rb.gravityScale = 1.0f - buoyancyFactor;
    }

    void Update()
    {
        // Implement sinusoidal sinking motion
        SineSink();
    }

    void SineSink()
    {
        // Increment time elapsed
        timeElapsed += Time.deltaTime;

        // Calculate the vertical sink offset using a sine function
        float sinkOffset = Mathf.Sin(timeElapsed * sinkFrequency) * sinkAmplitude;

        // Apply the offset to the Y position
        transform.position = new Vector3(transform.position.x, transform.position.y - sinkOffset, transform.position.z);
    }
}

