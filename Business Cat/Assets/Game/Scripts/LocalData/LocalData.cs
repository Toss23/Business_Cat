using System;
using UnityEngine;

public static class LocalData
{
    public static int LoadAttribute(Attribute attribute)
    {
        return PlayerPrefs.GetInt(attribute.ToString());
    }

    public static void SaveAttribute(Attribute attribute, int value)
    {
        PlayerPrefs.SetInt(attribute.ToString(), value);
    }

    public static void SaveItemPurchase(Item item)
    {
        SetBool(item.Identifier, true);
    }

    public static bool HaveItem(Item item)
    {
        return GetBool(item.Identifier);
    }

    public static string[] EquipedItems()
    {
        int count = Enum.GetNames(typeof(ItemType)).Length;
        string[] items = new string[count];

        for (int i = 0; i < count; i++)
            items[i] = PlayerPrefs.GetString("Equiped" + (ItemType)i, DataKey.Null);

        return items;
    }

    public static void SetBool(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
    }

    public static bool GetBool(string key)
    {
        return PlayerPrefs.GetInt(key, 0) == 1;
    }
}

public class DataKey
{
    public static string Null { get; } = "Null";
}