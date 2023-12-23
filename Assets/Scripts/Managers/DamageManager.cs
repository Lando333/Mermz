using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public float bubblesDamage;
    public float harpoonDamage;
    public float inkDamage;

    [SerializeField] GameObject bubblesField;
    [SerializeField] GameObject harpoonField;
    [SerializeField] GameObject inkField;


    private void Update()
    {
        if (bubblesDamage > 0) bubblesField.SetActive(true);
        if (harpoonDamage > 0) harpoonField.SetActive(true);
        if (inkDamage > 0) inkField.SetActive(true);

        GameManager.instance.currentBubblesDamage.text = bubblesDamage.ToString();
        GameManager.instance.currentHarpoonDamage.text = harpoonDamage.ToString();
        GameManager.instance.currentInkDamage.text = inkDamage.ToString();
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
