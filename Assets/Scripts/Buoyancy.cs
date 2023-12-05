using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Buoyancy : MonoBehaviour
{
    public float buoyancyFactor = 1.0f; // Adjust this to control buoyancy strength
    public float dashForce = 5.0f;      // Adjust this to control the dash force
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
        // Implement your swim dash logic here
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwimDash();
        }

        // Implement sinusoidal sinking motion
        SineSink();
    }

    void SwimDash()
    {
        // Get the mouse position in world coordinates
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction from the player to the mouse
        Vector2 dashDirection = (mousePosition - (Vector2)transform.position).normalized;

        // Add a force in the calculated direction to simulate a swim dash
        rb.AddForce(dashDirection * dashForce, ForceMode2D.Impulse);
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

