using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Buoyancy : MonoBehaviour
{
    public float sinkFrequency = 2.0f;  // Adjust this to control the sinking frequency
    public float sinkAmplitude = 1.0f;  // Adjust this to control the sinking amplitude

    private float timeElapsed = 0f;

    void Update()
    {
        // Implement sinusoidal sinking motion
        SineSink();
    }

    void SineSink()
    {
        if (GameManager.instance.currentState != GameManager.GameState.Gameplay) return;

        // Increment time elapsed
        timeElapsed += Time.deltaTime;

        // Calculate the vertical sink offset using a sine function
        float sinkOffset = Mathf.Sin(timeElapsed * sinkFrequency) * sinkAmplitude;

        // Apply the offset to the Y position
        transform.position = new Vector3(transform.position.x, transform.position.y - sinkOffset, transform.position.z);
    }
}

