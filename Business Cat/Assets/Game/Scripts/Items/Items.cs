using UnityEngine;

public class Items : MonoBehaviour
{
    private static Items instance;

    private ItemData[] items;

    private void Awake()
    {
        instance = this;
        items = Resources.LoadAll<ItemData>("Items");
    }

    public static ItemData Find(string identifier)
    {
        foreach (ItemData itemData in instance.items)
        {
            if (itemData.Identifier == identifier)
                return itemData;
        }
        return null;
    }
}