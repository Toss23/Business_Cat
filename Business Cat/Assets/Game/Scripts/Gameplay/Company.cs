using TMPro;
using UnityEngine;

public class Company : MonoBehaviour
{
    [HideInInspector] public Companies OnClickListener;
    [HideInInspector] public Companies OnUpdateListener;

    [Header("Main")]
    [SerializeField] private string identidier;
    [SerializeField] private int count;

    [Header("Price")]
    [SerializeField] private int priceHistoryLength;
    [SerializeField] private float updateTime;
    [SerializeField] private int priceMin;
    [SerializeField] private int priceMax;
    [SerializeField] [Range(0, 1)] private float priceSpread;
    private float timer;
    private int currentPrice;
    private int[] priceHistory;
    private int priceDelta { get { return priceMax - priceMin; } }

    [Header("View")]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private TMP_Text countText;

    public float PriceTrend { get; set; } = 0.5f;
    public int[] PriceHistory { get { return priceHistory; } }

    private void Awake()
    {
        priceHistory = new int[priceHistoryLength];
        for (int i = 0; i < priceHistoryLength; i++)
            priceHistory[i] = 0;
    }

    private void Update()
    {
        nameText.text = identidier;
        priceText.text = "Price: " + currentPrice;
        countText.text = "Count: " + count;

        timer += Time.deltaTime;
        if (timer >= updateTime)
        {
            timer -= updateTime;

            float random = Random.Range((PriceTrend - priceSpread) * priceDelta, (PriceTrend + priceSpread) * priceDelta);
            int delta = Mathf.RoundToInt(random);
            int value = priceMin + Mathf.Clamp(delta, 0, priceDelta);
            SetPrice(value);

            if (OnUpdateListener != null) OnUpdateListener.UpdateGraph();
        }
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
    }

    public void OnClick()
    {
        OnClickListener.Select(this);
    }
}
