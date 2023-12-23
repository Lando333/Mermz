using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGem : MonoBehaviour
{
    SpriteRenderer sprite;
    Color blue = new(0.2039216f, 0.6156863f, 1f, 0.3254902f);
    Color purple = new(0.8f, 0.2039216f, 255, 0.3254902f);
    public int counter;

    private string bonus = "Bonus";
    private int bonusAvailable;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = blue;

        bonusAvailable = PlayerPrefs.GetInt(bonus);
    }

    private void OnMouseDown()
    {
        counter++;

        if (sprite.color == blue) sprite.color = purple;
        else if (sprite.color == purple) sprite.color = blue;

        if (counter > 1000 && bonusAvailable == 0)
        {
            CurrencyManager cm;
            cm = FindObjectOfType<CurrencyManager>();
            cm.AddCurrency(1000);

            SetBonusUnavailable();
        }
    }

    public void SetBonusUnavailable()
    {
        PlayerPrefs.SetInt(bonus, 1);
        PlayerPrefs.Save();

        bonusAvailable = PlayerPrefs.GetInt(bonus);
    }
}
