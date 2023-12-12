using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyPearl : Pickup
{
    public int currencyGranted;

    public override void Collect()
    {
        if (hasBeenCollected)
        {
            return;
        }
        else
        {
            base.Collect();
        }

        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.IncreaseCurrency(currencyGranted);
    }
}
