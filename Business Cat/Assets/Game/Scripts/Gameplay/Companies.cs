using UnityEngine;

public class Companies : MonoBehaviour
{
    [SerializeField] private Company[] companies;
}

[System.Serializable]
public class Company
{
    [SerializeField] private string identidier;
    [SerializeField] private int price;
    [SerializeField] private int count;
}