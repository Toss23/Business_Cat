using UnityEngine;

public enum ItemType
{
    Top,
    Pants,
    Hat,
    Glasses
}

[CreateAssetMenu(fileName = "Item", menuName = "Gameplay/New Item")]
public class Item : ScriptableObject
{
    [SerializeField] private int index;
    [Space(10)]
    [SerializeField] private string identifier;
    [SerializeField] private string displayName;
    [SerializeField] private Sprite sprite;
    [SerializeField] private ItemType type;
    [Space(10)]
    [SerializeField] private Attribute attribute;
    [SerializeField] private int attributeValue;
    [Space(10)]
    [SerializeField] private GameObject prefab;

    public int Index { get { return index; } }
    public string Identifier { get { return identifier; } }
    public string DisplayName { get { return displayName; } }
    public Sprite Sprite { get { return sprite; } }
    public ItemType Type { get { return type; } }
    public Attribute Attribute { get { return attribute; } }
    public int AttributeValue { get { return attributeValue; } }
    public GameObject Prefab { get { return prefab; } }
}