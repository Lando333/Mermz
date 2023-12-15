using System;
using System.Collections;
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

    float currentDashCooldown = 0;

    // References
    public Rigidbody2D rb;
    public PlayerStats player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerStats>();
        lastMovedVector = new Vector2(1, 0f);   // Sets initial movement of projectile
    }

    void Update()
    {
        InputManagement();

        DashManagement();
    }

    void FixedUpdate()
    {
        Move();
    }

    void DashManagement()
    {
        currentDashCooldown -= Time.deltaTime;
        
        if (currentDashCooldown > 0) return;
        GameManager.instance.dashDisplayIcon.enabled = true;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Dash());
        }
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

    public void Swim(Vector2 direction)
    {
        // Move the player directly based on the dash direction
        transform.position += (Vector3)direction * player.DashForce * Time.deltaTime;
    }
    IEnumerator Dash()
    {
        // Get the mouse position in world coordinates
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction from the player to the mouse
        Vector2 dashDirection = (mousePosition - (Vector2)transform.position).normalized;

        // Disable Rigidbody2D gravity and regular movement during the dash
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;

        // Disable regular movement during the dash
        enabled = false;

        // Play dash sound effect


        GameManager.instance.dashDisplayIcon.enabled = false;

        // Perform the dash
        for (float t = 0; t < player.DashDuration; t += Time.deltaTime)
        {
            Swim(dashDirection);
            yield return null;
        }

        // Re-enable Rigidbody2D gravity and regular movement after the dash
        rb.gravityScale = 1;
        enabled = true;

        // Start dash cooldown
        currentDashCooldown = player.DashCooldown;
    }
}
