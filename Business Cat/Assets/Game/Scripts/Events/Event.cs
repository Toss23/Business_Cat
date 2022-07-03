using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "Gameplay/New Event")]
public class Event : ScriptableObject
{
    [SerializeField] private string displayName = "Event";
    [SerializeField] private int prestige;
    [SerializeField] private string shortDescription;
    [SerializeField] private int price;
    [Space(10)]
    [SerializeField] private Action[] actions;

    public string DisplayName { get { return displayName; } }
    public int Prestige { get { return prestige; } }
    public string ShortDescription { get { return shortDescription; } }
    public int Price { get { return price; } }
    public Action[] Actions { get { return actions; } }
}

[System.Serializable]
public class Action
{
    [SerializeField] private string displayName;
    [SerializeField] [Multiline] private string description;
    [SerializeField] private Variant[] variants;

    public string DisplayName { get { return displayName; } }
    public string Description { get { return description; } }
    public Variant[] Variants { get { return variants; } }
}

[System.Serializable]
public class Variant
{
    public enum Result
    {
        None, Money, Reputation, Stock
    }

    [SerializeField] private string displayName;
    [SerializeField] [Multiline] private string description;
    [Space(10)]
    [SerializeField] private Result result;
    [SerializeField] private int value;

    public string DisplayName { get { return displayName; } }
    public string Description { get { return description; } }
}