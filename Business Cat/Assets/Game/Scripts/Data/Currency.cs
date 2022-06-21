using TMPro;
using UnityEngine;

public class Currency : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private string identifier;
    [SerializeField] private int count;

    [Header("Config")]
    [SerializeField] private int countMax;
    [SerializeField] private TMP_Text text;

    public int Count { get { return count; } }

    private void Start()
    {
        UpdateText();
    }

    private void FixedUpdate()
    {
        UpdateText();
    }

    public bool Add(int value)
    {
        if (value > 0)
        {
            if (count + value <= countMax)
            {
                count += value;
                UpdateText();
                return true;
            }
        }
        return false;
    }

    public bool Remove(int value)
    {
        if (value > 0)
        {
            if (count - value >= 0)
            {
                count -= value;
                UpdateText();
                return true;
            }
        }
        return false;
    }

    private void UpdateText()
    {
        if (text != null) text.text = count + " " + identifier;
    }
}