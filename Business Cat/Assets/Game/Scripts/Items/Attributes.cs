using System.Collections.Generic;
using UnityEngine;

public enum Attribute
{
    None,
    Serious,
    Stylish,
    Sincere
}

public class Attributes : MonoBehaviour
{
    [SerializeField] private Items items;
    private Dictionary<Attribute, int> attributes;
    private Dictionary<Attribute, int> attributesFromEvents;
    private bool initialized;

    private void Awake()
    {
        attributes = new Dictionary<Attribute, int>();
        attributesFromEvents = new Dictionary<Attribute, int>();

        int count = System.Enum.GetNames(typeof(Attribute)).Length;
        for (int i = 0; i < count; i++)
        {
            attributes.Add((Attribute)i, LocalData.LoadAttribute((Attribute)i));
            attributesFromEvents.Add((Attribute)i, LocalData.LoadAttribute((Attribute)i));
        }

        items.OnLoad.Add(new Runnable(CreateAttributes));
    }

    private void CreateAttributes()
    {
        foreach (Item item in items.Array)
        {
            if (items.HaveItem(item))
                Add(item.Attribute, item.AttributeValue);
        }
        initialized = true;
    }

    public void Add(Attribute attribute, int value)
    {
        if (value > 0)
        {
            attributes[attribute] += value;
            if (initialized) attributesFromEvents[attribute] += value;
        }
    }

    public int Get(Attribute attribute)
    {
        return attributes[attribute];
    }

    public void Save()
    {
        int count = System.Enum.GetNames(typeof(Attribute)).Length;
        for (int i = 0; i < count; i++)
            LocalData.SaveAttribute((Attribute)i, attributesFromEvents[(Attribute)i]);
    }

    public int GetVariantChance(Variant variant)
    {
        int successChance = variant.MaxPercent;
        if (variant.AttributeValue != 0)
        {
            int deltaMinMaxPercent = variant.MaxPercent - variant.MinPercent;
            float attributeValue = attributes[variant.AttributeCheck];
            float attributePercent = Mathf.Clamp01(attributeValue / variant.AttributeValue);
            float percent = deltaMinMaxPercent * attributePercent + variant.MinPercent;
            successChance = Mathf.RoundToInt(percent);
        }
        return successChance;
    }
}