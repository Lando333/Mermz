using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyStats enemy;
    Transform player;

    Vector2 knockbackVelocity;
    float knockbackDuration;

    //[SerializeField] float minDistance = 0.25f; // Minimum distance to stop moving towards the player

    void Start()
    {
        enemy = GetComponent<EnemyStats>();
        player = FindObjectOfType<PlayerMovement>().transform;
    }


    void Update()
    {

        /*// Calculate the direction vector towards the player
        Vector3 directionToPlayer = player.position - transform.position;

        // Check if the distance is greater than the minimum required to move
        if (directionToPlayer.magnitude > minDistance)
        {
            // Normalize the direction vector and multiply by move speed
            Vector3 moveDirection = directionToPlayer.normalized;
            transform.position += moveDirection * enemy.currentMoveSpeed * Time.deltaTime;
        }*/

        // If we are currently being knocked back, then process knockback
        if (knockbackDuration > 0)
        {
            transform.position += (Vector3)knockbackVelocity * Time.deltaTime;
            knockbackDuration -= Time.deltaTime;
        }
        else
        {
            // Otherwise constantly move toward the player
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemy.currentMoveSpeed * Time.deltaTime);
        }
    }

    // Meant to be called from other scripts to create knockback
    public void Knockback(Vector2 velocity, float duration)
    {
        // Ignore knockback if the duration is greater than 0
        if (knockbackDuration > 0) return;

        // Begins the knockback
        knockbackVelocity = velocity;
        knockbackDuration = duration;
    }
}
