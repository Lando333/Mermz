using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PassiveItemScriptableObject",menuName ="ScriptableObjects/Passive Item")]
public class PassiveItemScriptableObject : ScriptableObject
{
    [SerializeField]
    float multiplier;
    public float Multiplier { get => multiplier; private set => multiplier = value; }


    [SerializeField]
    int level;          // Only modified in editor
    public int Level { get => level; private set => level = value; }

    [SerializeField]
    GameObject nextLevelPrefab; // Prefab of what the object will become after lvl up, NOT wave spawn
    public GameObject NextLevelPrefab { get => nextLevelPrefab; private set => nextLevelPrefab = value; }

    [SerializeField]
    new string name;
    public string Name { get => name; private set => name = value; }

    [SerializeField]
    string description; // Description of item; if item is an upgrade, place the description of the upgrades
    public string Description { get => description; private set => description = value; }

    [SerializeField]
    Sprite icon;        // Only modified in editor
    public Sprite Icon { get => icon; private set => icon = value; }
}
