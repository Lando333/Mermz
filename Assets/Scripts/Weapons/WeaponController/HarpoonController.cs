using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonController : WeaponController
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
            GameObject spawnedHarpoon = Instantiate(weaponData.Prefab);
            spawnedHarpoon.transform.position = transform.position;

            Vector2 direction = currentTarget - (Vector2)transform.position;

            // Set the reference to WeaponController in the spawned harpoon
            spawnedHarpoon.GetComponent<ProjectileWeaponBehavior>().SetWeaponController(this);

            spawnedHarpoon.GetComponent<ProjectileWeaponBehavior>().Direction = direction;
            spawnedHarpoon.GetComponent<HarpoonBehavior>().RotateProjectile();
        }
    }

}
