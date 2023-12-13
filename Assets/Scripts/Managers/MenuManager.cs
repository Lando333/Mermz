using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mermaidBox;
    [SerializeField] private GameObject diverBox;
    [SerializeField] private GameObject octopusManBox;

    private void Update()
    {
        if (CharacterSelector.instance.characterData.name == "Mermaid Character")
        {
            mermaidBox.SetActive(true);
            diverBox.SetActive(false);
            octopusManBox.SetActive(false);
        }
        else if (CharacterSelector.instance.characterData.name == "Diver Character")
        {
            mermaidBox.SetActive(false);
            diverBox.SetActive(true);
            octopusManBox.SetActive(false);
        }
        else if (CharacterSelector.instance.characterData.name == "Octopus Man Character")
        {
            mermaidBox.SetActive(false);
            diverBox.SetActive(false);
            octopusManBox.SetActive(true);
        }
    }

    public void SelectCharacter(CharacterScriptableObject character)
    {
        CharacterSelector.instance.characterData = character;
    }
}
