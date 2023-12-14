using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblesBehavior : ProjectileWeaponBehavior
{
    List<GameObject> markedEnemies;

    protected override void Start()
    {
        base.Start();
        markedEnemies = new List<GameObject>();
    }

    void Update()
    {
        transform.position += direction * currentSpeed * Time.deltaTime; //Sets the movement of the harpoon
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy") && !markedEnemies.Contains(col.gameObject))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(GetCurrentDamage());

            markedEnemies.Add(col.gameObject); // Marks enemy so they don't take damage from same instance of ink
        }
        else if (col.CompareTag("Prop"))
        {
            if (col.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(GetCurrentDamage());

                markedEnemies.Add(col.gameObject);
            }

        }
    }
}