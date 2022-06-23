using TMPro;
using UnityEngine;

public class BuySellButton : MonoBehaviour
{
    [SerializeField] private int count = 1;
    [SerializeField] private int countMax = 1000;

    [Space(20)]
    [SerializeField] private Companies companies;
    [SerializeField] private TMP_Text buyText;
    [SerializeField] private TMP_Text sellText;

    private void Start()
    {
        UpdateText();
    }

    public void Add(int value)
    {
        if (value > 0)
        {
            if (count + value <= countMax)
                count += value;
            else
                count = countMax;

            UpdateText();
        }
    }

    public void Remove(int value)
    {
        if (value > 0)
        {
            if (count - value >= 1)
                count -= value;
            else
                count = 1;

            UpdateText();
        }
    }

    private void UpdateText()
    {
        if (count == 1)
        {
            buyText.text = "Buy";
            sellText.text = "Sell";
        }
        else
        {
            buyText.text = "Buy\nx" + count;
            sellText.text = "Sell\nx" + count;
        }
    }

    public void OnClickBuy()
    {
        companies.OnClickBuy(count);
    }

    public void OnClickSell()
    {
        companies.OnClickSell(count);
    }
}