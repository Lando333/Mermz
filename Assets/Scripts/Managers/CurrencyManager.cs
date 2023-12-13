using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    protected int totalAmount { get; private set; }
    [HideInInspector] public int currentAmount = 0;

    private string currency = "Currency";

    [SerializeField] private TextMeshProUGUI totalDisplay;
    [SerializeField] private TextMeshProUGUI currentDisplay;

    private void Start()
    {
        totalAmount = PlayerPrefs.GetInt(currency);
    }

    private void Update()
    {
        if (totalDisplay == null)
        {
            currentDisplay.text = currentAmount.ToString();
        }
        else if (currentDisplay == null)
        {
            totalDisplay.text = totalAmount.ToString();
        }
    }

    public void AddCurrency(int amount)
    {
        totalAmount += amount;
        PlayerPrefs.SetInt(currency, totalAmount);
        PlayerPrefs.Save();
    }

    public void SubtractCurrency(int amount)
    {
        totalAmount -= amount;
        PlayerPrefs.SetInt(currency, totalAmount);
        PlayerPrefs.Save();
    }

    public void UpdateCurrency()
    {
        totalAmount += currentAmount;
        PlayerPrefs.SetInt(currency, totalAmount);
        PlayerPrefs.Save();
    }

    public void SetToZero()
    {
        totalAmount = 0;
        PlayerPrefs.SetInt(currency, totalAmount);
        PlayerPrefs.Save();
    }
}
