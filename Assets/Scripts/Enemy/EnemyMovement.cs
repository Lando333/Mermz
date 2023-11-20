using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyStats enemy;
    Transform player;

    [SerializeField] float minDistance = 0.25f; // Minimum distance to stop moving towards the player

    void Start()
    {
        enemy = GetComponent<EnemyStats>();
        player = FindObjectOfType<PlayerMovement>().transform;
    }


    void Update()
    {
        // Calculate the direction vector towards the player
        Vector3 directionToPlayer = player.position - transform.position;

        // Check if the distance is greater than the minimum required to move
        if (directionToPlayer.magnitude > minDistance)
        {
            // Normalize the direction vector and multiply by move speed
            Vector3 moveDirection = directionToPlayer.normalized;
            transform.position += moveDirection * enemy.currentMoveSpeed * Time.deltaTime;
        }
    }
}
