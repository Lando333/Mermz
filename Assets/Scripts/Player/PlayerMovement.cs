using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    [HideInInspector]
    public Vector2 moveDir;
    [HideInInspector]
    public Vector2 lastMovedVector;

    public float dashForce = 5.0f;      // Adjust this to control the dash force

    //References
    Rigidbody2D rb;
    PlayerStats player;


    void Start()
    {
        player = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        lastMovedVector = new Vector2(1, 0f);   // Sets initial movement of projectile
    }

    void Update()
    {
        InputManagement();

        // Implement your swim dash logic here
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwimDash();
        }

    }

    void FixedUpdate()
    {
        Move();
    }

    void InputManagement()
    {
        if (GameManager.instance.isGameOver || GameManager.instance.isPaused) return;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(moveX, moveY).normalized;

        if (moveDir.x != 0)
        {
            lastHorizontalVector = moveDir.x;
            lastMovedVector = new Vector2(lastHorizontalVector, 0f);    // Last moved X
        }
        
        if (moveDir.y != 0)
        {
            lastVerticalVector = moveDir.y;
            lastMovedVector = new Vector2(0f, lastVerticalVector);    // Last moved Y
        }

        if (moveDir.x != 0 && moveDir.y != 0)
        {
            lastMovedVector = new Vector2(lastHorizontalVector, lastVerticalVector);    // while moving
        }
    }

    void Move()
    {
        if (GameManager.instance.isGameOver) return;

        rb.velocity = new Vector2(moveDir.x * player.CurrentMoveSpeed, moveDir.y * player.CurrentMoveSpeed);
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
}
