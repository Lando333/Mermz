using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGem : MonoBehaviour
{
    SpriteRenderer sprite;
    Color blue = new(0.2039216f, 0.6156863f, 1f, 0.3254902f);
    Color purple = new(0.8f, 0.2039216f, 255, 0.3254902f);
    public int counter;
    private bool bonusAvailable = true;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = blue;
    }

    private void OnMouseDown()
    {
        counter++;

        if (sprite.color == blue) sprite.color = purple;
        else if (sprite.color == purple) sprite.color = blue;

        if (counter > 10 && bonusAvailable)
        {
            // something cool happens

            CurrencyManager cm;
            cm = FindObjectOfType<CurrencyManager>();
            cm.AddCurrency(1000);

            // Set in PlayerPrefs
            bonusAvailable = false;
        }
    }
}
