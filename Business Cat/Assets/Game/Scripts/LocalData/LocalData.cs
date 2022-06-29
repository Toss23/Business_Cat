using UnityEngine;

public static class LocalData
{
    public static void SaveItemPurchase(Item item)
    {
        SetBool(item.Identifier, true);
    }

    public static bool HaveItem(Item item)
    {
        return GetBool(item.Identifier);
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