using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    CharacterScriptableObject characterData;

    //Current stats
    float currentRecovery;
    float currentHealth;
    float currentMoveSpeed;
    float currentPower;
    float currentProjectileSpeed;
    float currentMagnet;

    #region Current Stats Properties
    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            // Check if the value has changed
            if (currentHealth != value)
            {
                currentHealth = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentHealthDisplay.text = MathF.Round(currentHealth) + "/" + characterData.MaxHealth;
                }
                // Add additional logic here that needs to be executed when value changes
            }
        }
    }

    public float CurrentRecovery
    {
        get { return currentRecovery; }
        set
        {
            // Check if the value has changed
            if (currentRecovery != value)
            {
                currentRecovery = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentRecoveryDisplay.text = currentRecovery.ToString(); ;
                }
                // Add additional logic here that needs to be executed when value changes
            }
        }
    }

    public float CurrentMoveSpeed
    {
        get { return currentMoveSpeed; }
        set
        {
            // Check if the value has changed
            if (currentMoveSpeed != value)
            {
                currentMoveSpeed = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMoveSpeedDisplay.text = currentMoveSpeed.ToString();
                }
                // Add additional logic here that needs to be executed when value changes
            }
        }
    }

    public float CurrentPower
    {
        get { return currentPower; }
        set
        {
            // Check if the value has changed
            if (currentPower != value)
            {
                currentPower = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentPowerDisplay.text = currentPower.ToString();
                }
                // Add additional logic here that needs to be executed when value changes
            }
        }
    }

    public float CurrentProjectileSpeed
    {
        get { return currentProjectileSpeed; }
        set
        {
            // Check if the value has changed
            if (currentProjectileSpeed != value)
            {
                currentProjectileSpeed = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentProjectileSpeedDisplay.text = currentProjectileSpeed.ToString();
                }
                // Add additional logic here that needs to be executed when value changes
            }
        }
    }

    public float CurrentMagnet
    {
        get { return currentMagnet; }
        set
        {
            // Check if the value has changed
            if (currentMagnet != value)
            {
                currentMagnet = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMagnetDisplay.text = currentMagnet.ToString();
                }
                // Add additional logic here that needs to be executed when value changes
            }
        }
    }

    #endregion
    
    //Dash stats
    float dashForce;
    float dashDuration;
    float dashCooldown;
    AudioClip dashSoundEffect;

    #region Dash Stats

    public float DashForce
    {
        get { return dashForce; }
        set
        {
            // Check if the value has changed
            if (dashForce != value)
            {
                dashForce = value;
                // Add additional logic here that needs to be executed when value changes
            }
        }
    }
    public float DashDuration
    {
        get { return dashDuration; }
        set
        {
            // Check if the value has changed
            if (dashDuration != value)
            {
                dashDuration = value;
                // Add additional logic here that needs to be executed when value changes
            }
        }
    }
    public float DashCooldown
    {
        get { return dashCooldown; }
        set
        {
            // Check if the value has changed
            if (dashCooldown != value)
            {
                dashCooldown = value;
                // Add additional logic here that needs to be executed when value changes
            }
        }
    }
    public AudioClip DashSoundEffect
    {
        get { return dashSoundEffect; }
        set
        {
            // Check if the value has changed
            if (dashSoundEffect != value)
            {
                dashSoundEffect = value;
                // Add additional logic here that needs to be executed when value changes
            }
        }
    }
    #endregion

    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int currentLevelCap;
    public int initialCap;

    // Used for defining level range and exp cap increase for that range
    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    //I-Frames
    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    public List<LevelRange> levelRanges;

    InventoryManager inventory;
    public int weaponIndex;
    public int passiveItemIndex;

    // Currency variables
    private CurrencyManager cm;

    [Header("UI")]
    public Image healthBar;
    public Image expBar;
    public TextMeshProUGUI levelText;

    [Header("Setup")]
    Animator anim;
    
    public GameObject secWeapon;
    public GameObject passItem1;
    public GameObject passItem2;

    void Awake()
    {
        characterData = CharacterSelector.GetData();
        CharacterSelector.instance.DestroySingleton();

        cm = FindObjectOfType<CurrencyManager>();

        inventory = GetComponent<InventoryManager>();

        CurrentHealth = characterData.MaxHealth;
        CurrentRecovery = characterData.Recovery;
        CurrentMoveSpeed = characterData.MoveSpeed;
        CurrentPower = characterData.Power;
        CurrentProjectileSpeed = characterData.ProjectileSpeed;
        CurrentMagnet = characterData.Magnet;

        DashForce = characterData.DashForce;
        DashDuration = characterData.DashDuration;
        DashCooldown = characterData.DashCooldown;
        DashSoundEffect = characterData.DashSoundEffect;

        SpawnWeapon(characterData.StartingWeapon);
        //SpawnWeapon(secWeapon);
        //SpawnPassiveItem(passItem1);
        SpawnPassiveItem(passItem2);

        anim = GetComponent<Animator>();
        
    }

    void Start()
    {
        currentLevelCap = initialCap;

        // Set the current stats display
        GameManager.instance.currentHealthDisplay.text = currentHealth + "/" + characterData.MaxHealth;
        GameManager.instance.currentRecoveryDisplay.text = currentRecovery.ToString();
        GameManager.instance.currentMoveSpeedDisplay.text = currentMoveSpeed.ToString();
        GameManager.instance.currentPowerDisplay.text = currentPower.ToString();
        GameManager.instance.currentProjectileSpeedDisplay.text = currentProjectileSpeed.ToString();
        GameManager.instance.currentMagnetDisplay.text = currentMagnet.ToString();

        GameManager.instance.AssignChosenCharacterUI(characterData);

        anim.runtimeAnimatorController = characterData.Animator;

        UpdateHealthBar();
        UpdateExpBar();
        UpdateLevelText();
    }

    void Update()
    {
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        // If the invincibility timer reaches 0, set the invincibility flag to false
        else if (isInvincible)
        {
            isInvincible = false;
        }

        Recover();
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;

        LevelUpChecker();

        UpdateExpBar();
    }

    public void IncreaseCurrency(int amount)
    {
        cm.currentAmount += amount;
    }

    private void LevelUpChecker()
    {
        if (experience >= currentLevelCap)
        {
            level++;
            experience -= currentLevelCap;

            int experienceCapIncrease = 0;
            foreach (LevelRange range in levelRanges)
            {
                if (level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }
            currentLevelCap += experienceCapIncrease;

            UpdateLevelText();

            GameManager.instance.StartLevelUp();
        }
    }

    void UpdateExpBar()
    {
        // Update exp fill amount
        expBar.fillAmount = (float)experience / currentLevelCap;
    }

    void UpdateLevelText()
    {
        // Updates character's level display
        levelText.text = level.ToString();
    }

    public void TakeDamage(float dmg)
    {
        if (!isInvincible)
        {
            CurrentHealth -= dmg;

            invincibilityTimer = invincibilityDuration;
            isInvincible = true;

            if (CurrentHealth <= 0)
            {
                Kill();
            }

            UpdateHealthBar();
        }
    }

    void UpdateHealthBar()
    {
        // Update health bar
        healthBar.fillAmount = currentHealth / characterData.MaxHealth;
    }

    private void Kill()
    {
        if (!GameManager.instance.isGameOver)
        {
            GameManager.instance.AssignLevelReachedUI(level);
            GameManager.instance.AssignChosenWeaponsAndPassiveItemsUI(inventory.weaponUISlots, inventory.passiveItemUISlots);
            GameManager.instance.GameOver();
        }
    }

    public void RestoreHealth(float amount)
    {
        // Only heal player if current health is less than max
        if (CurrentHealth < characterData.MaxHealth)
        {
            CurrentHealth += amount;

            // Only heal to max and not over
            if (CurrentHealth > characterData.MaxHealth)
            {
                CurrentHealth = characterData.MaxHealth;
            }

            UpdateHealthBar();
        }
    }

    void Recover()
    {
        if (CurrentHealth < characterData.MaxHealth)
        {
            CurrentHealth += CurrentRecovery * Time.deltaTime;
            
            if (CurrentHealth > characterData.MaxHealth)
            {
                CurrentHealth = characterData.MaxHealth;
            }
        }
    }

    public void SpawnWeapon(GameObject weapon)
    {
        // Checking if inventory is full
        if (weaponIndex >= inventory.weaponSlots.Count -1)
        {
            print("Inventory already full.");
            return; 
        }

        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);    // Spawn the starting weapon
        spawnedWeapon.transform.SetParent(transform);                                               // Sets weapon as child of player
        inventory.AddWeapon(weaponIndex, spawnedWeapon.GetComponent<WeaponController>());           // Adds weapon to it's inventory slot

        weaponIndex++;
    }

    public void SpawnPassiveItem(GameObject passiveItem)
    {
        if (passiveItemIndex >= inventory.passiveItemSlots.Count - 1)
        {
            print("Inventory slots already full.");
            return;
        }

        GameObject spawnedPassiveItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
        spawnedPassiveItem.transform.SetParent(transform);
        inventory.AddPassiveItem(passiveItemIndex, spawnedPassiveItem.GetComponent<PassiveItem>());

        passiveItemIndex++;
    }
}
