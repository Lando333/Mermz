using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    public WeaponScriptableObject weaponData;
    float currentCooldown;
    bool autoAttack = true;

    protected PlayerMovement pm;

    protected virtual void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        currentCooldown = weaponData.CooldownDuration;
    }


    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        
        ToggleAttackMode();
        
        if (autoAttack)
        {
            Attack();
        }

        else
        {
            ManualAttack();
        }
    }

    private void ToggleAttackMode()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            autoAttack = !autoAttack;
        }
    }

    private void ManualAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (currentCooldown <= 0f)
        {
            ResetCooldown();
        }
    }

    protected virtual void ResetCooldown()
    {
        currentCooldown = weaponData.CooldownDuration;
    }
}
