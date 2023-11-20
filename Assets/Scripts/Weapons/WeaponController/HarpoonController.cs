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
        base.Attack();
        GameObject spawnedHarpoon = Instantiate(weaponData.Prefab);
        spawnedHarpoon.transform.position = transform.position;  // Assign position to same as this obj, parented to player
        spawnedHarpoon.GetComponent<HarpoonBehavior>().DirectionChecker(pm.lastMovedVector); // Reference and set the direction
    }
}
