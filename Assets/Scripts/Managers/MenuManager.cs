using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private GameObject selectorBox;
    [SerializeField] private TextMeshProUGUI flavorText;

    [Header("Characters")]
    [SerializeField] private GameObject diverSilhouette;
    [SerializeField] private TextMeshProUGUI diverName;
    [SerializeField] private GameObject octopusManSilhouette;
    [SerializeField] private TextMeshProUGUI octopusManName;

    bool diverUnlocked;
    bool octoManUnlocked;

    private void Start()
    {
        LoadPlayableCharacters();
        LoadFlavorText();
    }

    private void Update()
    {
        if (CharacterSelector.instance.characterData.name == "Mermaid Character")
        {
            selectorBox.transform.position = new Vector3(440f, 427f, selectorBox.transform.position.z);
        }
        else if (CharacterSelector.instance.characterData.name == "Diver Character")
        {
            selectorBox.transform.position = new Vector3(640f, 427f, selectorBox.transform.position.z);
        }
        else if (CharacterSelector.instance.characterData.name == "Octopus Man Character")
        {
            selectorBox.transform.position = new Vector3(840f, 427f, selectorBox.transform.position.z);
        }
    }

    public void SelectCharacter(CharacterScriptableObject character)
    {
        CharacterSelector.instance.characterData = character;
        LoadFlavorText();
    }

    public void LoadPlayableCharacters() 
    {
        if (diverUnlocked)
        {
            diverSilhouette.SetActive(false);
            diverName.text = "Dan";
        }
        else
        {
            diverSilhouette.SetActive(true);
            diverName.text = "???";
        }

        if (octoManUnlocked)
        {
            octopusManSilhouette.SetActive(false);
            octopusManName.text = "Octopus Man";
        }
        else
        {
            octopusManSilhouette.SetActive(true);
            octopusManName.text = "???";
        }
    }

    public void LoadFlavorText()
    {
        flavorText.text = CharacterSelector.instance.characterData.CharacterFlavorText;
    }
}
