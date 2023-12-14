using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblesBehavior : ProjectileWeaponBehavior
{
    List<GameObject> markedEnemies;

    [SerializeField] float speedDecayRate = 0.1f; // Adjust the rate of speed decay
    [SerializeField] float floatUpSpeed = 1.0f; // Adjust the speed at which bubbles float up after stopping

    private bool hasStoppedMoving = false;

    protected override void Start()
    {
        base.Start();
        markedEnemies = new List<GameObject>();
    }

    void Update()
    {
        if (!hasStoppedMoving)
        {
            // Decrement the speed over time
            currentSpeed -= speedDecayRate * Time.deltaTime;

            // Ensure the speed doesn't go below zero
            currentSpeed = Mathf.Max(currentSpeed, 0f);

            transform.position += direction * currentSpeed * Time.deltaTime; // Sets the movement of the harpoon

            // Check if the bubbles have stopped moving
            if (currentSpeed <= 0.25f)
            {
                hasStoppedMoving = true;
            }
        }
        else
        {
            // Float the bubbles up after they stop moving
            transform.position += Vector3.up * floatUpSpeed * Time.deltaTime;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy") && !markedEnemies.Contains(col.gameObject))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(GetCurrentDamage());

            markedEnemies.Add(col.gameObject); // Marks enemy so they don't take damage from same instance of bubbles
        }
        else if (col.CompareTag("Prop"))
        {
            if (col.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(GetCurrentDamage());

                markedEnemies.Add(col.gameObject);
            }

        }
    }
}