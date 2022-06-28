using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] private Item[] items;
    [SerializeField] private List<Runnable> onLoad;

    private void Start()
    {
        Debug.Log("[Items] Loading items...");

        items = Resources.LoadAll<Item>("Items");
        items = Sort(items);

        Debug.Log("[Items] Items loaded: " + items.Length);

        if (onLoad != null)
        {
            for (int i = 0; i < onLoad.Count; i++)
                onLoad[i].Run();
        }
    }

    private Item[] Sort(Item[] items)
    {
        int count = items.Length;
        for (int j = 0; j < count; j++)
        {
            for (int i = 0; i < count - 1; i++)
            {
                Item a = items[i];
                Item b = items[i + 1];

                if (a.Index > b.Index)
                {
                    items[i] = b;
                    items[i + 1] = a;
                }
            }
            count--;
        }
        return items;
    }

    public Item[] FindWithType(ItemType type)
    {
        List<Item> items = new List<Item>();

        foreach (Item item in this.items)
        {
            if (item.Type == type)
                items.Add(item);
        }

        return items.ToArray();
    }

    public Item Find(string identifier)
    {
        foreach (Item itemData in items)
        {
            if (itemData.Identifier == identifier)
                return itemData;
        }
        return null;
    }

    public void AddRunnableOnLoad(Runnable runnable)
    {
        if (onLoad == null) onLoad = new List<Runnable>();
        onLoad.Add(runnable);
    }
}