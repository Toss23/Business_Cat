using TMPro;
using UnityEngine;

public class Company : MonoBehaviour
{
    [HideInInspector] public Companies OnClickListener;
    [HideInInspector] public Companies OnUpdatePrice;

    [Header("Main")]
    [SerializeField] private string identidier;
    [SerializeField] private int count;

    [Header("Price")]
    [SerializeField] private int priceHistoryLength;
    [SerializeField] private int priceMin;
    [SerializeField] private int priceMax;
    [SerializeField] [Range(0, 1)] private float priceSpread;
    private int currentPrice;
    private int[] priceHistory;
    private int priceDelta { get { return priceMax - priceMin; } }

    [Header("View")]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private TMP_Text countText;

    public string Identidier { get { return identidier; } }
    public float PriceTrend { get; set; } = 0.5f;
    public int[] PriceHistory { get { return priceHistory; } }
    public int Price { get { return currentPrice; } }

    private void Awake()
    {
        priceHistory = new int[priceHistoryLength];
        for (int i = 0; i < priceHistoryLength; i++)
            priceHistory[i] = 0;
    }

    private void Start()
    {
        float random = Random.Range((PriceTrend - priceSpread) * priceDelta, (PriceTrend + priceSpread) * priceDelta);
        int delta = Mathf.RoundToInt(random);
        int value = priceMin + Mathf.Clamp(delta, 0, priceDelta);
        SetPrice(value);
        UpdateText();
    }

    private void UpdateText()
    {
        nameText.text = identidier;
        priceText.text = "Price: " + currentPrice;
        countText.text = "Count: " + count;
    }

    private void SetPrice(int value)
    {
        currentPrice = value;
        for (int i = 0; i < priceHistoryLength; i++)
        {
            if (i != priceHistoryLength - 1)
                priceHistory[i] = priceHistory[i + 1];
            else
                priceHistory[i] = value;
        }
        if (OnUpdatePrice != null) OnUpdatePrice.UpdateGraph();
    }

    public void OnClick()
    {
        OnClickListener.Select(this);
    }

    public bool Add(int value)
    {
        if (value > 0)
        {
            if (count + value <= 1000000)
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
}
