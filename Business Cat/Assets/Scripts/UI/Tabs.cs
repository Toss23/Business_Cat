using UnityEngine;

public class Tabs : MonoBehaviour
{
    [SerializeField] private Tab[] tabs;
}

[System.Serializable]
public class Tab
{
    public enum Swap
    {
        Not, LeftAndRight, Left, Right
    }

    [SerializeField] private string tabName;
    [SerializeField] private Vector2 position;
    [SerializeField] private Swap enableSwap;
}