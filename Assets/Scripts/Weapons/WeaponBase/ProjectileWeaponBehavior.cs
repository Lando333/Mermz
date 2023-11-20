using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponBehavior : MonoBehaviour
{
    public WeaponScriptableObject weaponData;

    protected Vector3 direction;
    public float destroyAfterSeconds;

    // Current stats
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected int currentPierce;

    void Awake()
    {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
    }

    public float GetCurrentDamage()
    {
        return currentDamage *= FindObjectOfType<PlayerStats>().CurrentMight;
    }

    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }


    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;

        float dirX = direction.x;
        float dirY = direction.y;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        if (dirX < 0 && dirY == 0)      // left
        {
            scale.x = scale.x * -1;
        }
        else if (dirX == 0 && dirY < 0) // down
        {
            scale.y = scale.y * -1;
            rotation.z = -90f;
        }
        else if (dirX == 0 && dirY > 0) // up
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            rotation.z = -90f;
        }
        else if (dirX > 0 && dirY > 0) // right up
        {
            rotation.z = 45f;
        }
        else if (dirX > 0 && dirY < 0) // right down
        {
            rotation.z = -45f;
        }
        else if (dirX < 0 && dirY > 0) // left up
        {
            scale.x = scale.x * -1;
            rotation.z = -45f;
        }
        else if (dirX < 0 && dirY < 0) // left down
        {
            scale.x = scale.x * -1;
            rotation.z = 45f;
        }

        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);
    }
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(GetCurrentDamage());    //Using currentDamage instead of weaponData.damage bc of damage multipliers
            ReducePierce();
        }
        else if (col.CompareTag("Prop"))
        {
            if (col.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(GetCurrentDamage());
                ReducePierce();
            }
        }
    }

    void ReducePierce()
    {
        currentPierce--;
        if (currentPierce <= 0)
        {
            Destroy(gameObject);
        }
    }
}
