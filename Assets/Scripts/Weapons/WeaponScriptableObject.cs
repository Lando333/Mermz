using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="WeaponScriptableObject", menuName ="ScriptableObjects/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject prefab;
    public GameObject Prefab { get => prefab; private set => prefab = value; }

    // Base stats for weapons
    [SerializeField]
    float damage;
    public float Damage { get => damage; private set => damage = value; }

    [SerializeField]
    float speed;
    public float Speed { get => speed; private set => speed = value; }

    [SerializeField]
    float cooldownDuration;
    public float CooldownDuration { get => cooldownDuration; private set => cooldownDuration = value; }

    [SerializeField]
    int pierce;
    public int Pierce { get => pierce; private set => pierce = value; }

    [SerializeField]
    int level;          // Only modified in editor
    public int Level { get => level; private set => level = value; }
    
    [SerializeField]
    GameObject nextLevelControllerPrefab; // Prefab of what the object will become after lvl up, NOT wave spawn
    public GameObject NextLevelPrefab { get => nextLevelControllerPrefab; private set => nextLevelControllerPrefab = value; }

    [SerializeField]
    new string name;
    public string Name { get => name; private set => name = value; }

    [SerializeField]
    string description; // Description of weapon; if weapon is an upgrade, place the description of the upgrades
    public string Description { get => description; private set => description = value; }

    [SerializeField]
    Sprite icon;        // Only modified in editor
    public Sprite Icon { get => icon; private set => icon = value; }
}
