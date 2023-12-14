using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblesController : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }
    
    protected override void Attack()
    {
        if (currentCooldown <= 0)
        {
            base.Attack();
            GameObject spawnedBubble = Instantiate(weaponData.Prefab);
            spawnedBubble.transform.position = transform.position;

            Vector2 direction = currentTarget - (Vector2)transform.position;

            // Set the reference to WeaponController in the spawned harpoon
            spawnedBubble.GetComponent<ProjectileWeaponBehavior>().SetWeaponController(this);

            spawnedBubble.GetComponent<ProjectileWeaponBehavior>().Direction = direction;
            spawnedBubble.GetComponent<BubblesBehavior>().RotateProjectile();
        }
    }
}
