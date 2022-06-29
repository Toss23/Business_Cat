using System.Collections.Generic;
using UnityEngine;

public enum Attribute
{
    None,
    Serious,
    Stylish
}

public class Attributes : MonoBehaviour
{
    [SerializeField] private Items items;
    private Dictionary<Attribute, int> attributes;

    private void Awake()
    {
        attributes = new Dictionary<Attribute, int>();

        int count = System.Enum.GetNames(typeof(Attribute)).Length;
        for (int i = 0; i < count; i++)
            attributes.Add((Attribute)i, 0);

        items.AddRunnableOnLoad(new Runnable(CreateAttributes));
    }

    private void CreateAttributes()
    {
        foreach (Item item in items.Array)
        {
            if (items.HaveItem(item))
                Add(item.Attribute, item.AttributeValue);
        }
    }

    public void Add(Attribute attribute, int value)
    {
        if (value > 0)
        {
            attributes[attribute] += value;
        }
    }

    public int Get(Attribute attribute)
    {
        return attributes[attribute];
    }
}