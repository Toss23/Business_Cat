using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Currency : MonoBehaviour
{
    public static string Money { get; private set; } = "Money";
    public static string Donate { get; private set; } = "Donate";

    private enum Type
    {
        Default,
        Money,
        Donate
    }

    private static List<Currency> currencies; 

    [Header("Main")]
    [SerializeField] private string identifier;
    [SerializeField] private Type type;
    [SerializeField] private int count;
    [SerializeField] private int countMax;
    [SerializeField] private bool useSuffix;

    [Header("View")]
    [SerializeField] private TMP_Text text;

    public int Count { get { return count; } }

    private void Awake()
    {
        if (currencies == null) currencies = new List<Currency>();
        currencies.Add(this);

        switch (type)
        {
            case Type.Money:
                Money = identifier;
                break;
            case Type.Donate:
                Donate = identifier;
                break;
        }
    }

    private void Update()
    {
        UpdateText();
    }

    public bool Add(int value)
    {
        if (value >= 0)
        {
            if (count + value <= countMax)
            {
                count += value;
                UpdateText();
                return true;
            }
        }
        return false;
    }

    public bool Remove(int value)
    {
        if (value >= 0)
        {
            if (count - value >= 0)
            {
                count -= value;
                UpdateText();
                return true;
            }
        }
        return false;
    }

    private void UpdateText()
    {
        if (text != null)
        {
            text.text = count.ToString();
            if (useSuffix) text.text += " " + identifier;
        }
    }

    public static Currency Find(string identifier)
    {
        foreach (Currency currency in currencies)
        {
            if (identifier == currency.identifier)
                return currency;
        }
        return null;
    }
}