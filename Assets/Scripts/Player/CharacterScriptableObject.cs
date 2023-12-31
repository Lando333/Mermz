using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CharacterScriptableObject", menuName ="ScriptableObjects/Character")]
public class CharacterScriptableObject : ScriptableObject
{
    [Header("Setup")]
    [SerializeField]
    Sprite icon;
    public Sprite Icon { get => icon; private set => icon = value; }

    [SerializeField]
    Sprite silhouette;
    public Sprite Silhouette { get => silhouette; private set => silhouette = value; }

    [SerializeField]
    RuntimeAnimatorController animator;
    public RuntimeAnimatorController Animator { get => animator; private set => animator = value; }

    [SerializeField]
    new string name;
    public string Name { get => name; private set => name = value; }

    [SerializeField]
    GameObject startingWeapon;
    public GameObject StartingWeapon { get => startingWeapon; private set => startingWeapon = value; }

    // Base stats for the character
    [Header("Base Stats")]
    [SerializeField]
    float maxHealth;
    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }

    [SerializeField]
    float recovery;
    public float Recovery { get => recovery; private set => recovery = value; }

    [SerializeField]
    float moveSpeed;
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }

    [SerializeField]
    float power;
    public float Power { get => power; private set => power = value; }

    [SerializeField]
    float projectileSpeed;
    public float ProjectileSpeed { get => projectileSpeed; private set => projectileSpeed = value; }

    [SerializeField]
    float magnet;
    public float Magnet { get => magnet; private set => magnet = value; }

    [Header("Dash Settings")]

    [SerializeField]
    float dashForce;
    public float DashForce { get => dashForce; private set => dashForce = value; }

    [SerializeField]
    float dashDuration;
    public float DashDuration { get => dashDuration; private set => dashDuration = value; }

    [SerializeField]
    float dashCooldown;
    public float DashCooldown { get => dashCooldown; private set => dashCooldown = value; }

    [SerializeField]
    AudioClip dashSoundEffect;
    public AudioClip DashSoundEffect { get => dashSoundEffect; private set => dashSoundEffect = value; }

    [Header("Flavor Settings")]

    [SerializeField]
    [TextArea(10, 20)]
    string characterFlavorText;
    public string CharacterFlavorText { get => characterFlavorText; private set => characterFlavorText = value; }

}
