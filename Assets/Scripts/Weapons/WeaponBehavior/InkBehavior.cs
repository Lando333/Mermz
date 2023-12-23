using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkBehavior : MeleeWeaponBehavior
{
    List<GameObject> markedEnemies;

    public string weaponType = "Ink";

    private float currentDmg;

    protected override void Start()
    {
        base.Start();
        markedEnemies = new List<GameObject>();

        currentDmg = GetCurrentDamage();
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy") && !markedEnemies.Contains(col.gameObject))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDmg, weaponType);

            markedEnemies.Add(col.gameObject); // Marks enemy so they don't take damage from same instance of ink
        }
        else if (col.CompareTag("Prop"))
        {
            if (col.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(currentDmg);

                markedEnemies.Add(col.gameObject);
            }
        }
    }
}
