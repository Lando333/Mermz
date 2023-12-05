using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    public WeaponScriptableObject weaponData;
    protected float currentCooldown;

    protected PlayerMovement pm;
    protected PlayerController pc;
    public Vector2 currentTarget;
    Vector2 mousePos;


    protected virtual void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        pc = FindObjectOfType<PlayerController>();
        currentCooldown = weaponData.CooldownDuration;
    }

    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime; 
        
        if (pc.autoAttack)
        {
            Attack();
        }
        else
        {
            ManualAttack();
        }

        if (pc.autoAim)
        {
            AutoTarget();
        }
        else
        {
            AimAtMouse();
        }
    }
    private void ManualAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }
    protected virtual void Attack()
    {
        if (currentCooldown <= 0f)
        {
            ResetCooldown();
        }
    }
    private void ResetCooldown()
    {
        currentCooldown = weaponData.CooldownDuration;
    }
    private void AutoTarget()
    {
        GameObject closestEnemy = FindClosestEnemy();

        if (closestEnemy != null)
        {
            // Set closestEnemy as the target
            currentTarget = closestEnemy.transform.position;
        }
    }
    private void AimAtMouse()
    {
        // Get the mouse position in the world space
        mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Set mousePos as the target
        currentTarget = mousePos;
    }
    private GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
        {
            // No enemies found
            return null;
        }

        Transform weaponTransform = transform; // Assuming this script is attached to the weapon
        Vector3 weaponPosition = weaponTransform.position;

        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            // Calculate the distance between the weapon and the enemy
            float distance = Vector3.Distance(weaponPosition, enemy.transform.position);

            // Check if this enemy is closer than the current closest enemy
            if (distance < closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }

        return closestEnemy;
    }
}
