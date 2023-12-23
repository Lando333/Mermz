using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;

    //Current stats
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentDamage;

    public float despawnDistance = 20f;
    Transform player;

    [Header("Damage Feedback")]
    public Color damageColor = new Color(1, 0, 0, 1);   // The color of the damage flash
    public float damageFlashDuration = .2f;             // How long the flash lasts
    public float deathFadeTime = .6f;                   // Time it takes for enemy to fade
    [SerializeField] Material whiteFlashMaterial;
    SpriteRenderer sr;
    Color originalColor;
    Material originalMaterial;

    EnemyMovement movement;
    DamageManager damageManager;



    void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
    }

    void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        originalMaterial = sr.material;

        movement = GetComponent<EnemyMovement>();
        damageManager = FindObjectOfType<DamageManager>();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) >= despawnDistance)
        {
            ReturnEnemy();
        }
    }

    void ReturnEnemy()
    {
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        transform.position = player.position + es.relativeSpawnPoints[UnityEngine.Random.Range(0, es.relativeSpawnPoints.Count)].position;
    }

    public void TakeDamage(float dmg, string weaponType)
    {
        currentHealth -= dmg;
        StartCoroutine(DamageFlash());

        if (weaponType == "Bubbles") damageManager.AddBubblesDamage(dmg);
        else if (weaponType == "Harpoon") damageManager.AddHarpoonDamage(dmg);
        else if (weaponType == "Ink") damageManager.AddInkDamage(dmg);
        else
        {
            print(weaponType);
            damageManager.AddInkDamage(dmg);
        }

        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    IEnumerator DamageFlash()
    {
        sr.material = whiteFlashMaterial;
        sr.color = Color.white;

        yield return new WaitForSeconds(damageFlashDuration);

        sr.material = originalMaterial;
        sr.color = originalColor;
    }

    private void Kill()
    {
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerStats player = col.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);
        }
    }

    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return;     // Fixes drops spawn error in editor

        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        es.OnEnemyKilled();
    }
}
