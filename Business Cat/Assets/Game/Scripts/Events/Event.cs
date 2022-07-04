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

    public Action GetRandomAction()
    {
        int weights = 0;
        foreach (Action action in actions)
            weights += action.Weight;

        int prevWeight = 0;
        int random = Random.Range(0, weights);
        for (int i = 0; i < actions.Length; i++)
        {
            if (random >= prevWeight & random < prevWeight + actions[i].Weight)
                return actions[i];

            prevWeight += actions[i].Weight;
        }
        return null;
    }
}

[System.Serializable]
public class Action
{
    [SerializeField] private string displayName;
    [SerializeField] private int weight;
    [SerializeField] [Multiline] private string description;
    [SerializeField] private Variant[] variants;

    public string DisplayName { get { return displayName; } }
    public int Weight { get { return weight; } }
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