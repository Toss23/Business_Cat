using UnityEngine;

public enum Attribute
{
    None,       // ���
    Serious,    // ���������
    PartyGoer,  // ��������
    Generous,   // ������
    Sincere,    // ��������
    Stylish     // ��������
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item Data")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string identifier;
    [SerializeField] private Attribute attribute;
    [SerializeField] private int attributeValue;

    public string Identifier { get { return identifier; } }
    public Attribute Attribute { get { return attribute; } }
    public int AttributeValue { get { return attributeValue; } }
}