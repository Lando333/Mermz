using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkController : WeaponController
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
            GameObject spawnedInk = Instantiate(weaponData.Prefab);
            spawnedInk.transform.position = transform.position;
            spawnedInk.transform.parent = transform;
        }
    }
}
