using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponBehavior : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    protected PlayerMovement pm;
    private WeaponController weaponController;

    protected Vector3 direction;
    public Vector2 Direction
    {
        get { return direction; }
        set { direction = value.normalized; }
    }


    // Current stats
    public float destroyAfterSeconds;
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
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    public void SetWeaponController(WeaponController controller)
    {
        weaponController = controller;
    }

    public float GetCurrentDamage()
    {
        return currentDamage *= FindObjectOfType<PlayerStats>().CurrentMight;
    }

    public void RotateProjectile()
    {
        if (weaponController != null)
        {
            direction = (weaponController.currentTarget - (Vector2)transform.position).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
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
