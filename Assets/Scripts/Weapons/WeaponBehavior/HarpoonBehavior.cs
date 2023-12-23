using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonBehavior : ProjectileWeaponBehavior
{
    public string weaponType = "Harpoon";

    private float currentDmg;

    protected override void Start()
    {
        base.Start();

        currentDmg = GetCurrentDamage();
    }


    void Update()
    {
        transform.position += direction * currentSpeed * Time.deltaTime; //Sets the movement of the harpoon
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDmg, weaponType);    //Using currentDamage instead of weaponData.damage bc of damage multipliers
            ReducePierce();
        }
        else if (col.CompareTag("Prop"))
        {
            if (col.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(currentDmg);
                ReducePierce();
            }
        }
    }
}
