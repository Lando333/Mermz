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

    [SerializeField] GameObject bubblesField;
    [SerializeField] GameObject harpoonField;
    [SerializeField] GameObject inkField;

    private void Start()
    {
        if (GameManager.instance.chosenCharacterName.text == mermaidName) bubblesField.SetActive(true);
        else if (GameManager.instance.chosenCharacterName.text == diverName) harpoonField.SetActive(true);
        else if (GameManager.instance.chosenCharacterName.text == octopusManName) inkField.SetActive(true);
    }

    private void Update()
    {
        if (bubblesDamage > 0) bubblesField.SetActive(true);
        if (harpoonDamage > 0) harpoonField.SetActive(true);
        if (inkDamage > 0) inkField.SetActive(true);

        GameManager.instance.currentBubblesDamage.text = Mathf.RoundToInt(bubblesDamage).ToString();
        GameManager.instance.currentHarpoonDamage.text = Mathf.RoundToInt(harpoonDamage).ToString();
        GameManager.instance.currentInkDamage.text = Mathf.RoundToInt(inkDamage).ToString();
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
