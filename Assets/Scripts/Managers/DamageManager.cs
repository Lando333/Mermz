using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public float bubblesDamage;
    public float harpoonDamage;
    public float inkDamage;

    private string mermaidName = "Anet";
    private string diverName = "Dan";
    private string octopusManName = "Octopus Man";

    [Header("Pause Screen Objects")]
    [SerializeField] GameObject bubblesField;
    [SerializeField] GameObject harpoonField;
    [SerializeField] GameObject inkField;

    [Header("Results Screen Objects")]
    [SerializeField] GameObject bubblesFinal;
    [SerializeField] GameObject harpoonFinal;
    [SerializeField] GameObject inkFinal;

    private void Start()
    {
        // Turn on chosen character's weapon by default
        if (GameManager.instance.chosenCharacterName.text == mermaidName) bubblesField.SetActive(true);
        else if (GameManager.instance.chosenCharacterName.text == diverName) harpoonField.SetActive(true);
        else if (GameManager.instance.chosenCharacterName.text == octopusManName) inkField.SetActive(true);
    }

    private void Update()
    {
        // If weapon is not already on and is now being used, turn it on
        if (bubblesField.activeInHierarchy == false && bubblesDamage > 0) bubblesField.SetActive(true);
        if (harpoonField.activeInHierarchy == false && harpoonDamage > 0) harpoonField.SetActive(true);
        if (inkField.activeInHierarchy == false && inkDamage > 0) inkField.SetActive(true);

        // Update current damage displays
        GameManager.instance.currentBubblesDamage.text = Mathf.RoundToInt(bubblesDamage).ToString();
        GameManager.instance.currentHarpoonDamage.text = Mathf.RoundToInt(harpoonDamage).ToString();
        GameManager.instance.currentInkDamage.text = Mathf.RoundToInt(inkDamage).ToString();
    }

    public void FinalDamageDisplay()
    {
        // Turn weapon display on if it has been used
        if (bubblesDamage > 0) bubblesFinal.SetActive(true);
        if (harpoonDamage > 0) harpoonFinal.SetActive(true);
        if (inkDamage > 0) inkFinal.SetActive(true);

        // Update weapon damage displays
        GameManager.instance.finalBubblesDamage.text = Mathf.RoundToInt(bubblesDamage).ToString();
        GameManager.instance.finalHarpoonDamage.text = Mathf.RoundToInt(harpoonDamage).ToString();
        GameManager.instance.finalInkDamage.text = Mathf.RoundToInt(inkDamage).ToString();
    }

    public void AddBubblesDamage(float amount)
    {
        bubblesDamage += amount;
    }

    public void AddHarpoonDamage(float amount)
    {
        harpoonDamage += amount;
    }

    public void AddInkDamage(float amount)
    {
        inkDamage += amount;
    }
}
